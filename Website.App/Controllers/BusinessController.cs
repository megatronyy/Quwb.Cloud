using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lib.Framework.Core.Helpers;
using Newtonsoft.Json;
using Website.App.Common;

namespace Website.App.Controllers
{
    public class BusinessController : Controller
    {
        public IActionResult Index()
        {
            string strUrl = ApiManager.GetApiUrl(ApiManager.Api_GetBusinessTypeById, "2");
            string strBusiness = HttpRequestHelper.HttpGet(strUrl);
            dynamic obj = JsonHelper.JsonTo<dynamic>(strBusiness);
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