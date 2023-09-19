using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebWMS.Common;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.DTO.Customers;
using WebWMS.Core.Services.CustomersService;
using WebWMS.Models;
using Newtonsoft.Json;
using WebWMS.Core.Repositorys.Collections;
using System.Drawing.Printing;
using WebWMS.CommonLibraries.Encrypt;
using CommonLibraries.Excel;
using System.Data;
using System.Threading;

namespace WebWMS.Controllers
{
    public class UserController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<Customer> logger;
        private readonly IMapper mapper;
        private readonly IHostEnvironment hostEnvironment;

        public UserController(ICustomerService customerService, ILogger<Customer> logger, IMapper mapper, IHostEnvironment hostEnvironment)
        {
            this.customerService = customerService;
            this.logger = logger;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {

            return View();
        }



        public async Task<string> GetCustomerList(RequestModel model)
        {
            ResultMessage<IPagedList<CustomerDto>> result = new ResultMessage<IPagedList<CustomerDto>>();
            try
            {
                result.Status = 200;
                result.Source = await customerService.GetAllPageListAsync(model.pageIndex - 1, model.pageSize, model.Where);
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
        public async Task<string> Add(UserViewModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var cuto = mapper.Map<CustomerDto>(model);
                cuto.IsRemove = model.IsRemove;
                cuto.IsEnabled = model.IsEnabled;
                cuto.PassWord = HelpCrypto.DESEncrypt(cuto.PassWord);
                cuto.CreateTime = DateTime.Now.ToString();
                cuto.Creator = User.Identity?.Name;
                if (await customerService.GetCustomerByAccountAsync(cuto.Account))
                {
                    result.Message = "账号已存在不能重复添加!";
                    result.Status = -1;
                }
                int num = await customerService.InsertCustomerAsync(cuto);
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
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Update(UserViewModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var cu = await customerService.GetCustomerByIdAsync(model.Id);
                cu.Address = model.Address;
                cu.Email = model.Email;
                cu.MoblePhone = model.MoblePhone;
                cu.IsEnabled = model.IsEnabled;
                cu.IsRemove = model.IsRemove;
                cu.UpdateTime = DateTime.Now.ToString();
                var cuto = mapper.Map<CustomerDto>(cu);
                int num = await customerService.UpdateCustomerAsync(cuto);
                if (num > 0)
                {
                    result.Message = "修改成功!";
                    result.Status = 200;

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
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Delete(int id)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                int num = await customerService.DeleteCustomerAsync(id);
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
            List<CustomerDto>? list = null;
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
                    list = JsonConvert.DeserializeObject<List<CustomerDto>>(json);
                }
                if (list != null && list.Count > 0)
                {
                    bool isRepeat = list.GroupBy(i => i.Account).Any(g => g.Count() > 1);
                    if (isRepeat)
                    {
                        result.Message = "Excel表格中账号有重复请检查!";
                        ViewBag.ImportExceMessage = result.Message;
                        ViewBag.ImportExceStatus = result.Status;
                        return View("Index");
                    }
                    var dic = await customerService.CustomeAnyAsync(list);
                    if (dic.FirstOrDefault().Key)
                    {
                        result.Status = -1;
                        result.Message = "账号" + dic.FirstOrDefault().Value + "已存在,不能重复导入!";
                        ViewBag.ImportExceMessage = result.Message;
                        ViewBag.ImportExceStatus = result.Status;
                        return View("Index");
                    }
                    list.ForEach(u => { u.PassWord = HelpCrypto.DESEncrypt("123456"); u.Creator = User?.Identity?.Name; u.IsEnabled = true; u.CreateTime = DateTime.Now.ToString(); });
                    num = await customerService.BatchInsertCustomerAsync(list, cancellationToken);
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
                string path = @"C:\项目\UserInfo-" + Guid.NewGuid().ToString() + ".xlsx";
                var list = await customerService.GetAllAsync();
                DataTable dt = EPPlusHelper.ListToDt(list);
                bool b = await EPPlusHelper.ExportExcel(path, dt,cancellationToken);
                if (b)
                {
                    result.Status = 200;
                    result.Message = $"导出成功!文件导出位置:{path}";
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
