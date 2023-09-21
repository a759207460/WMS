using AutoMapper;
using CommonLibraries.Redis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebWMS.Common;
using WebWMS.Core.DTO.Companys;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.CompanysService;
using WebWMS.Core.Services.MenusService;
using WebWMS.Models;

namespace WebWMS.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly RedisClientHelper redisClient;
        private readonly IMapper mapper;

        public CompanyController(ICompanyService companyService, RedisClientHelper redisClient, IMapper mapper)
        {
            this.companyService = companyService;
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
                companyto.CreateTime = DateTime.Now.ToString();
                if (await companyService.GetCompanyByNameAsync(companyto.CompanyCode))
                {
                    result.Message = "公司已存在不能重复添加!";
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
                companyto.CreateTime = DateTime.Now.ToString();
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
    }
}
