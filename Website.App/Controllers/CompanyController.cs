﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Website.App.Controllers
{
    public class CompanyController : Controller
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