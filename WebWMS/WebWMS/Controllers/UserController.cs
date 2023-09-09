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

namespace WebWMS.Controllers
{
    public class UserController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<Customer> logger;
        private readonly IMapper mapper;

        public UserController(ICustomerService customerService, ILogger<Customer> logger, IMapper mapper)
        {
            this.customerService = customerService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<string> GetCustomerList(int pageIndex = 1, int pageSize = 10)
        {
            ResultMessage<IPagedList<CustomerDto>> result = new ResultMessage<IPagedList<CustomerDto>>();
            try
            {
                result.Status = 200;
                result.Source = await customerService.GetAllAsync(pageIndex - 1, pageSize);
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

    }
}
