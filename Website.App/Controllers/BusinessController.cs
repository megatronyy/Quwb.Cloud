using Lib.Framework.Core.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using Website.ApiInvoke.Entity;
using Website.App.Common;
using Website.App.ViewModel;
using Website.Models;
using Website.Models.Request;

namespace Website.App.Controllers
{
    public class BusinessController : BaseController
    {
        private ApiInvoke.ApiClient _apiClient;

        public BusinessController()
        {
            _apiClient = new ApiInvoke.ApiClient("ApiConfig");
        }

        #region 登录
        [HttpGet]
        public IActionResult Index()
        {
            //如果已经登录，跳转到订单页
            if (ViewBag.UserAccount != null)
                return Redirect("/business/order");

            LoginViewModel model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            var requestModel = _apiClient.ApiGet<UserAccount>(string.Format("mobile={0}&pwd={1}", model.UserMobile, model.Password),
                "/user/login");

            if (requestModel.isSuccess && requestModel.code == 0)
            {
                var user = new ClaimsPrincipal(
                    new ClaimsIdentity(new[]
                    {
                            new Claim(ClaimTypes.Name, requestModel.data.username),
                            new Claim(ClaimTypes.MobilePhone, requestModel.data.mobile),
                            new Claim(ClaimTypes.UserData, JsonHelper.JsonTo<UserAccount>(requestModel.data)),
                    },
                    CookieAuthenticationDefaults.AuthenticationScheme));

                HttpContext.SignInAsync(Startup.CookiesName, user, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddHours(12),
                    IsPersistent = true,
                    AllowRefresh = false
                });

                return Redirect("/business/order");
            }
            else
            {
                model.Code = requestModel.code;
                model.Msg = requestModel.message;
            }

            return View(model);
        }
        #endregion

        #region 商机中心
        [Authorize]
        public IActionResult Order()
        {
            return View();
        }

        public IActionResult MyPub()
        {
            UserData userData = GetUserData();
            return View(userData);
        }

        public IActionResult MyAdd()
        {
            UserData userData = GetUserData();
            return View(userData);
        }

        public IActionResult MyBusiness()
        {
            UserData userData = GetUserData();
            return View(userData);
        }

        private UserData GetUserData()
        {
            UserData userData = null;
            UserAccount userAccount = JsonHelper.JsonTo<UserAccount>(ViewBag.UserAccount);
            if (userAccount != null)
            {
                var result = _apiClient.ApiPost<UserRequst, UserData>(new UserRequst() { userId = userAccount.userid }, "/user/info");
                if (result.isSuccess)
                {
                    userData = result.data;
                }
            }

            return userData;
        }
        #endregion

        #region 获取商机
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        #endregion 

        #region 发布商机
        [Authorize]
        public IActionResult Pub()
        {
            return View();
        }
        #endregion

        #region 注册帐号
        [HttpGet]
        public IActionResult Register()
        {
            RegisterModel model = new RegisterModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Mobile))
            {
                model.Code = -1;
                model.Msg = "请输入手机号";
            }
            else if (string.IsNullOrEmpty(model.UserName))
            {
                model.Code = -1;
                model.Msg = "请输入用户名";
            }
            else if (string.IsNullOrEmpty(model.Password))
            {
                model.Code = -1;
                model.Msg = "请输入密码";
            }
            else if (model.Password.Length < 6)
            {
                model.Code = -1;
                model.Msg = "密码长度必须大于6";
            }
            else if (model.CheckCode < 1000)
            {
                model.Code = -1;
                model.Msg = "请输入验证码";
            }
            else if (model.Password != model.RePassword)
            {
                model.Code = -1;
                model.Msg = "两次输入密码不一致";
            }

            if (model.Code == 0)
            {
                //验证验证码
                var requstModel = _apiClient.ApiPost<MobileCodeInfo, int>(
                    new MobileCodeInfo()
                    {
                        id = 0,
                        mobile = model.Mobile,
                        code = model.CheckCode,
                        createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    }, "/user/vercode");

                if (requstModel.isSuccess && requstModel.code == 0)
                {
                    requstModel = _apiClient.ApiPost<UserAccount, int>(
                    new UserAccount()
                    {
                        userid = 0,
                        username = model.UserName,
                        openid = "",
                        password = model.Password,
                        mobile = model.RePassword,
                        lastlogintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        isactive = 1
                    }, "/user/add");

                    if (requstModel.isSuccess && requstModel.code == 0)
                    {
                        return Redirect("/business/index");
                    }
                    else
                    {
                        model.Code = requstModel.code;
                        model.Msg = requstModel.message;
                    }
                }
                else
                {
                    model.Code = requstModel.code;
                    model.Msg = requstModel.message;
                }
            }
            return View(model);
        }

        public JsonResult SendMsg(string mobile)
        {
            Random R = new Random();
            int code = R.Next(1000, 9999);
            string strIp = "127.0.0.1";
            //保存验证码
            var result = _apiClient.ApiPost<MobileCodeInfo, int>(
                new MobileCodeInfo()
                {
                    id = 0,
                    mobile = mobile,
                    code = code,
                    mobileip = strIp,
                    createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                }, "/user/setcode");
            //保存短信内容
            if (result.isSuccess && result.code == 0)
            {
                result = _apiClient.ApiPost<MsgInfo, int>(
                    new MsgInfo()
                    {
                        msgid = 0,
                        mobile = mobile,
                        msgcontent = string.Format("欢迎注册惠商机，您的验证码是{0}，请在页面中填写验证码完成验证。", code),
                        msgip = strIp,
                        sendstatus = 0,
                        sendtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        sourceid = 1,
                        createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        isactive = 1
                    }, "/user/send");
            }

            return Json(result);
        }
        #endregion
    }
}