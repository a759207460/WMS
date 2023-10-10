using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Drawing;
using System.Security.Claims;
using System.Web;
using WebWMS.Common;
using WebWMS.Models;
using WebWMS.CommonLibraries.File;
using WebWMS.CommonLibraries.Encrypt;
using CommonLibraries.Redis;
using CommonLibraries.Excel;
using System.Data;
using WebWMS.Core.Services.UserInfosService;

namespace WebWMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly IUserInfoService customerService;
        private readonly RedisClientHelper redisClient;

        public LoginController(IMemoryCache cache, IUserInfoService customerService, RedisClientHelper redisClient)
        {
            this.cache = cache;
            this.customerService = customerService;
            this.redisClient = redisClient;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> Login(LoginViewModel model, CancellationToken cancellationToken)
        {

            //DataTable dt = new DataTable();
            //EPPlusHelper.ExportModleExcel(dt, cancellationToken);
            //string josn=await EPPlusHelper.ReadExcel(@"C:\Users\75920\test1.xlsx", cancellationToken);
            //var ls =JsonConvert.DeserializeObject<List<FileAccountModel>>(josn);
            string name = string.Empty;
            bool t = redisClient.SetString("name", model.Account);
            if (t)
            {
                name = redisClient.GetString("name");
            }
            ResultMessage result = new ResultMessage();
            try
            {
                string n = name;
                string code = cache.Get<string>("code")?.ToLower();
                if (code != model.ValidateCode.ToLower())
                {
                    result.Status = -1;
                    result.Message = "验证码错误!";
                    return JsonConvert.SerializeObject(result);
                }
                string pwd = HelpCrypto.DESEncrypt(model.PassWord);
                var cu = await customerService.GetCustomerAsync(model.Account, pwd);
                if (cu != null && cu.Id > 0)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Sid,cu.Id.ToString()),
                        new Claim(ClaimTypes.Dsa, cu.Account),
                        new Claim(ClaimTypes.Name,cu.Name ),
                        new Claim(ClaimTypes.MobilePhone,cu.MoblePhone),
                        new Claim(ClaimTypes.Email,cu.Email ),
                        new Claim(ClaimTypes.StreetAddress,cu.Address)
                    };//记录需要存储的数据
                    var claimnsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(new ClaimsPrincipal(claimnsIdentity), new AuthenticationProperties { IsPersistent = true });
                    result.Status = 200;
                    result.Message = "OK";
                    if (model.Remember)
                    {
                        IEnumerable<FileAccountModel> fileAccounts = new List<FileAccountModel>() {
                         new FileAccountModel { Account=model.Account,Password=model.PassWord,CreatTime=DateTime.Now }
                        };
                        HelpFile.WriteFile("C:\\Users\\75920\\test.txt", fileAccounts);
                    }
                }
                else
                {
                    result.Status = -1;
                    result.Message = "账户或密码错误!";
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
        /// 获取上次登录用户密码信息
        /// </summary>
        /// <returns></returns>
        public string GetRemember()
        {
            ResultMessage<FileAccountModel> result = new ResultMessage<FileAccountModel>();
            try
            {
                var dic = HelpFile.ReadFile<FileAccountModel>("C:\\Users\\75920\\test.txt");
                var ac = dic.OrderByDescending(f => f.CreatTime).FirstOrDefault();
                result.Status = 200;
                result.Source = ac;
            }
            catch (Exception ex)
            {
                result.Status = -1;
                result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 退出账号
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LogOut()
        {
            var claimnsIdentity = new ClaimsIdentity(HttpContext.User.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrinciple = new ClaimsPrincipal(claimnsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple);
            return Redirect("/");
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult GetValidateCode()
        {
            string code = HelpSecurityCode.MakeCode(4);
            cache.Set<string>("code", code, TimeSpan.FromMinutes(15));
            MemoryStream ms = HelpSecurityCode.CreateCodeImg(code);
            return File(ms.ToArray(), @"image/jpeg");
        }

    }
}
