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
        public IActionResult Index()
        {
            //如果已经登录，跳转到订单页
            if(ViewBag.UserAccount != null)
                return Redirect("/business/order");

            ResponseEntity<UserAccount> model = new ResponseEntity<UserAccount>()
            {
                isSuccess = true,
                code = 0,
                message = ""
            };

            if (Request.Method.ToLower() == "post")
            {
                string txtMobile = Request.Form["txtMobile"];
                string txtPwd = Request.Form["txtPwd"];

                model = _apiClient.ApiGet<UserAccount>(string.Format("mobile={0}&pwd={1}", txtMobile, txtPwd),
                    "/user/login");

                if (model.isSuccess && model.code == 0)
                {
                    var user = new ClaimsPrincipal(
                        new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, model.data.username),
                            new Claim(ClaimTypes.MobilePhone, model.data.mobile),
                            new Claim(ClaimTypes.UserData, JsonHelper.JsonTo<UserAccount>(model.data)),
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
                else {
                    model.data.username = txtMobile;
                    model.data.password = txtPwd;
                }
            }

            return View(model);
        }
        #endregion

        #region 商机列表
        [Authorize]
        public IActionResult Order()
        {
            UserData data = null;
            var result = _apiClient.ApiPost<UserRequst, UserData>(new UserRequst() { userId = 100000 }, "/user/info");
            if (result.isSuccess)
            {
                data = result.data;
            }
            //return View(data);
            return View();
        }
        #endregion

        #region 获取商机
        public IActionResult Add()
        {
            return View();
        }
        #endregion 

        #region 发布商机
        public IActionResult Pub()
        {
            return View();
        }
        #endregion

        #region 注册帐号
        public IActionResult Register()
        {
            ResponseEntity<int> model = new ResponseEntity<int>()
            {
                isSuccess = true,
                code = 0,
                message = ""
            };

            if (Request.Method.ToLower() == "post")
            {
                string txtMobile = Request.Form["txtMobile"];
                string txtName = Request.Form["txtName"];
                string txtPwd = Request.Form["txtPwd"];
                string txtRePwd = Request.Form["txtRePwd"];
                string txtCheckCode = Request.Form["txtCheckCode"];
                if (string.IsNullOrEmpty(txtMobile))
                {
                    model.code = -1;
                    model.message = "请输入手机号";
                }
                else if (string.IsNullOrEmpty(txtName))
                {
                    model.code = -1;
                    model.message = "请输入用户名";
                }
                else if (string.IsNullOrEmpty(txtPwd))
                {
                    model.code = -1;
                    model.message = "请输入密码";
                }
                else if (txtPwd.Length < 6)
                {
                    model.code = -1;
                    model.message = "密码长度必须大于6";
                }
                else if (string.IsNullOrEmpty(txtCheckCode))
                {
                    model.code = -1;
                    model.message = "请输入验证码";
                }
                else if (txtPwd != txtRePwd)
                {
                    model.code = -1;
                    model.message = "两次输入密码不一致";
                }

                if (model.code == 0)
                {
                    //验证验证码
                    model = _apiClient.ApiPost<MobileCodeInfo, int>(
                        new MobileCodeInfo()
                        {
                            id = 0,
                            mobile = txtMobile,
                            code = int.Parse(txtCheckCode),
                            createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        }, "/user/vercode");

                    if (model.isSuccess && model.code == 0)
                    {
                        model = _apiClient.ApiPost<UserAccount, int>(
                        new UserAccount()
                        {
                            userid = 0,
                            username = txtName,
                            openid = "",
                            password = txtPwd,
                            mobile = txtMobile,
                            lastlogintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            updatetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            isactive = 1
                        }, "/user/add");

                        if (model.isSuccess && model.code == 0)
                        {
                            return Redirect("/business/index");
                        }
                    }
                    else {

                    }
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