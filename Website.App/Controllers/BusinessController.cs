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
            var result = _apiClient.ApiPost<UserRequstEntity, UserResponseEntity>(new UserRequstEntity() { UserId = 10000 },"/user/info");
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