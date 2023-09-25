using AutoMapper;
using CommonLibraries.Excel;
using CommonLibraries.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using WebWMS.Common;
using WebWMS.CommonLibraries.Encrypt;
using WebWMS.Core.DTO.Companys;
using WebWMS.Core.DTO.CompanysDto;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.DTO.UserInfosDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.CompanysService;
using WebWMS.Core.Services.MenusService;
using WebWMS.Models;

namespace WebWMS.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IOptionsSnapshot<ExceConfig> options;
        private readonly RedisClientHelper redisClient;
        private readonly IMapper mapper;

        public CompanyController(ICompanyService companyService, IOptionsSnapshot<ExceConfig> options, RedisClientHelper redisClient, IMapper mapper)
        {
            this.companyService = companyService;
            this.options = options;
            this.redisClient = redisClient;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> GetCompanyList(RequestModel model)
        {
            ResultMessage<IPagedList<CompanyDto>> result = new ResultMessage<IPagedList<CompanyDto>>();
            try
            {
                result.Status = 200;
                result.Source = await companyService.GetAllPagedListAsync(model.pageIndex - 1, model.pageSize, model.Where);
            }
            catch (Exception ex)
            {
                result.Status = -1;
                result.Message = ex.Message;
            }
            string js = JsonConvert.SerializeObject(result);
            return js;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> Add(CompanyViewModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var companyto = mapper.Map<CompanyDto>(model);
                companyto.CompanyName = model.CompanyName;
                companyto.CompanyCode = model.CompanyCode;
                companyto.CompanyAddress = model.CompanyAddress;
                companyto.CompanyCity = model.CompanyCity;
                companyto.CompanyPrincipal = model.CompanyPrincipal;
                companyto.CompanyContact = model.CompanyContact;
                companyto.CreateTime = DateTime.Now.ToString();
                if (await companyService.GetCompanyByNameAsync(companyto.CompanyCode,companyto.CompanyName))
                {
                    result.Message = "公司编号或名称已存在不能重复添加!";
                    result.Status = -1;
                }
                int num = await companyService.InsertCompanyAsync(companyto);
                if (num > 0)
                {
                    result.Message = "新增成功!";
                    result.Status = 200;
                }
                else
                {
                    result.Status = -1; result.Message = "新增失败!";
                }
            }
            catch (Exception ex)
            {
                result.Status = -1; result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Update(CompanyViewModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var companyto = mapper.Map<CompanyDto>(model);
                companyto.CompanyName = model.CompanyName;
                companyto.CompanyCode = model.CompanyCode;
                companyto.CompanyAddress = model.CompanyAddress;
                companyto.CompanyCity = model.CompanyCity;
                companyto.CompanyPrincipal = model.CompanyPrincipal;
                companyto.CompanyContact = model.CompanyContact;
                companyto.UpdateTime = DateTime.Now.ToString();
                var company = mapper.Map<CompanyDto>(companyto);
                int num = await companyService.UpdateCompanyAsync(company);
                if (num > 0)
                {
                    result.Message = "修改成功!";
                    result.Status = 200;
                    await redisClient.DeleteHashOfAsync("WMS_MenuList", "menuhash");
                }
                else
                {
                    result.Status = -1; result.Message = "修改失败!";
                }
            }
            catch (Exception ex)
            {
                result.Status = -1; result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Delete(int id)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                int num = await companyService.DeleteCompanyAsync(id);
                if (num > 0)
                {
                    result.Message = "删除成功!";
                    result.Status = 200;

                }
                else
                {
                    result.Status = -1; result.Message = "删除失败!";
                }
            }
            catch (Exception ex)
            {
                result.Status = -1; result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }


        #region 导入导出

        /// <summary>
        /// excel 导入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ImportExcel([FromForm(Name = "file")] IFormFile excelFile, CancellationToken cancellationToken)
        {
            //CancellationTokenSource t = new CancellationTokenSource();
            //t.CancelAfter(30000);
            //CancellationToken s = t.Token;
            List<CompanyDto>? list = null;
            int num = 0;
            ResultMessage result = new ResultMessage();
            try
            {
                //string filePath = hostEnvironment.ContentRootPath;
                //string path = Path.Combine(filePath, "userInfo.xlsx");
                //FileInfo file = new FileInfo(path);
                if (excelFile == null || excelFile.Length <= 0)
                {
                    result.Message = "请选择导入文件!";
                    result.Status = -1;
                    ViewBag.ImportExceMessage = result.Message;
                    ViewBag.ImportExceStatus = result.Status;
                    return View("Index");
                }
                Stream stream = excelFile.OpenReadStream();
                string json = await EPPlusHelper.ReadExcel(stream, cancellationToken);
                if (json != null)
                {
                    var  elist = JsonConvert.DeserializeObject<List<ImportCompanyDto>>(json);
                    list = mapper.Map<List<CompanyDto>>(elist);
                }
                if (list != null && list.Count > 0)
                {
                    bool isRepeat = list.GroupBy(i => i.CompanyCode).Any(g => g.Count() > 1);
                    if (isRepeat)
                    {
                        result.Message = "Excel表格中账号有重复请检查!";
                        ViewBag.ImportExceMessage = result.Message;
                        ViewBag.ImportExceStatus = result.Status;
                        return View("Index");
                    }
                    var dic = await companyService.CompanyAnyAsync(list);
                    if (dic.FirstOrDefault().Key)
                    {
                        result.Status = -1;
                        result.Message = "账号" + dic.FirstOrDefault().Value + "已存在,不能重复导入!";
                        ViewBag.ImportExceMessage = result.Message;
                        ViewBag.ImportExceStatus = result.Status;
                        return View("Index");
                    }
                    list.ForEach(u => {u.CreateTime = DateTime.Now.ToString(); });
                    num = await companyService.BatchInsertCompanyAsync(list, cancellationToken);
                }
                if (num > 0)
                {
                    result.Status = 200;
                    result.Message = "导入成功!";
                }
                else
                {
                    result.Status = -1;
                    result.Message = "导入失败!";
                }

            }
            catch (Exception ex)
            {
                result.Status = -1;
                result.Message = ex.Message;
            }
            ViewBag.ImportExceMessage = result.Message;
            ViewBag.ImportExceStatus = result.Status;
            return View("Index");
        }

        /// <summary>
        /// 导出exce
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> ExportExcel(CancellationToken cancellationToken)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var list = await companyService.GetExportAsync();
                DataTable dt = EPPlusHelper.ListToDt(list);
                string modelPath = options.Value.ModelPath + "CompanyModel.xlsx";
                string rand = "-" + new Random().Next(1, 1000).ToString();
                string downloadPath = options.Value.DownloadPath + "Company-" + DateTime.Now.ToString("yyyy-MM-dd") + rand + ".xlsx";
                bool b = await EPPlusHelper.ExportModleExcel(modelPath, downloadPath, dt, cancellationToken);
                if (b)
                {
                    result.Status = 200;
                    result.Message = $"导出成功!文件导出位置:{downloadPath}";
                }
                else
                {
                    result.Status = -1;
                    result.Message = "导出失败!";
                }
            }
            catch (Exception ex)
            {
                result.Status = -1;
                result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }
        #endregion
    }
}
