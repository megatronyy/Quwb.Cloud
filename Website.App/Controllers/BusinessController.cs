using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lib.Framework.Core.Helpers;
using Newtonsoft.Json;
using Website.App.Common;
using Website.App.Models.Request;
using Website.App.Models.Response;

namespace Website.App.Controllers
{
    public class BusinessController : Controller
    {
        private ApiInvoke.ApiClient _apiClient;

        public BusinessController() {
            _apiClient = new ApiInvoke.ApiClient("ApiConfig");
        }
        public IActionResult Index()
        {
            var result = _apiClient.ApiPost<UserRequstEntity, dynamic>(new UserRequstEntity() { userId = 100000 },"/user/info");
            if (result.isSuccess)
            {
                dynamic data = result.data;
                if (data != null)
                {
                    int userid = data.userid;
                }
            }
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult Articles()
        {
            return View();
        }
    }
}