﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebWMS.Common;
using WebWMS.Models;
using Newtonsoft.Json;
using WebWMS.Core.Repositorys.Collections;
using System.Drawing.Printing;
using WebWMS.CommonLibraries.Encrypt;
using CommonLibraries.Excel;
using System.Data;
using System.Threading;
using Microsoft.Extensions.Options;
using WebWMS.Core.DTO.UserInfosDto;
using WebWMS.Core.Domain.Users;
using WebWMS.Core.Services.UserInfosService;
using Microsoft.AspNetCore.Authorization;
using WebWMS.Core.Services.RolesService;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Domain.Roles;
using System.Collections.Generic;

namespace WebWMS.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserInfoService customerService;
        private readonly IOptionsSnapshot<ExceConfig> options;
        private readonly ILogger<UserInfo> logger;
        private readonly IMapper mapper;
        private readonly IRoleService roleService;
        private readonly IHostEnvironment hostEnvironment;

        public UserController(IUserInfoService customerService, IOptionsSnapshot<ExceConfig> options, ILogger<UserInfo> logger, IMapper mapper, IRoleService roleService, IHostEnvironment hostEnvironment)
        {
            this.customerService = customerService;
            this.options = options;
            this.logger = logger;
            this.mapper = mapper;
            this.roleService = roleService;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {

            return View();
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> GetCustomerList(RequestModel model)
        {
            ResultMessage<IPagedList<UserInfoDto>> result = new ResultMessage<IPagedList<UserInfoDto>>();
            string json = string.Empty;
            try
            {
                result.Status = 200;
                result.Source = await customerService.GetAllPageListAsync(model.pageIndex - 1, model.pageSize, model.Where);
                //循环序列化设置
                var setting = new JsonSerializerSettings();
                //将循环引用的对象部进行序列化设置
                //setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; 

                //如果想要循环引用的数据得以保留，以便后面反序列化时能还原数据，所以将循环引用设置为序列化
                setting.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                setting.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                json = JsonConvert.SerializeObject(result, setting);
            }
            catch (Exception ex)
            {
                result.Status = -1;
                result.Message = ex.Message;
            }

            return json;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetRoleList()
        {
            ResultMessage<List<RoleDto>> result = new ResultMessage<List<RoleDto>>();
            try
            {
                var rlist = await roleService.GetAllAsync();
                result.Status = 200;
                result.Source = rlist;
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
        public async Task<string> Add(UserViewModel model, CancellationToken cancellation)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                UserInfoDto cuto = new UserInfoDto();
                cuto.Address = model.Address;
                cuto.Account = model.Account;
                cuto.Email = model.Email;
                cuto.MoblePhone = model.MoblePhone;
                cuto.Name = model.Name;
                cuto.IsRemove = model.IsRemove;
                cuto.IsEnabled = model.IsEnabled;
                cuto.PassWord = HelpCrypto.DESEncrypt(model.PassWord);
                cuto.CreateTime = DateTime.Now.ToString();
                cuto.Creator = User.Identity?.Name;
                List<UserAndRolesDto> ur = new List<UserAndRolesDto>();
                for (int i = 0; i < model.RoleList.Count; i++)
                {
                    UserAndRolesDto userAndRoles = new UserAndRolesDto()
                    {
                        RoleId = model.RoleList[i],
                        UserId = model.Id
                    };
                    ur.Add(userAndRoles);
                }
                cuto.Roles = ur;
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
                List<UserAndRolesDto> ur = new List<UserAndRolesDto>();
                for (int i = 0; i < model.RoleList.Count; i++)
                {
                    UserAndRolesDto userAndRoles = new UserAndRolesDto()
                    {
                        RoleId = model.RoleList[i],
                        UserId = model.Id
                    };
                    ur.Add(userAndRoles);
                }
                cu.Roles = ur;
                var cuto = mapper.Map<UserInfoDto>(cu);
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
                result.Status = -1;
                result.Message = ex.Message;
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
            List<UserInfoDto>? list = null;
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
                    var elist = JsonConvert.DeserializeObject<List<ImportUserInfoDto>>(json);
                    list = mapper.Map<List<UserInfoDto>>(elist);
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
                var list = await customerService.GetExportAsync();
                DataTable dt = EPPlusHelper.ListToDt(list);
                string modelPath = options.Value.ModelPath + "model.xlsx";
                string rand = "-" + new Random().Next(1, 1000).ToString();
                string downloadPath = options.Value.DownloadPath + "User-" + DateTime.Now.ToString("yyyy-MM-dd") + rand + ".xlsx";
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
