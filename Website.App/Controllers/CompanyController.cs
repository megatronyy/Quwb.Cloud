using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Website.App.Common;

namespace Website.App.Controllers
{
    public class CompanyController : BaseController
    {
        private ApiInvoke.ApiClient _apiClient;

        public CompanyController()
        {
            _apiClient = new ApiInvoke.ApiClient("ApiConfig");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}