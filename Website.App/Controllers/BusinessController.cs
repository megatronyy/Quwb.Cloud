using Microsoft.AspNetCore.Mvc;
using Website.Models;
using Website.Models.Request;

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
            //string struri = "https://enterbj.zhongchebaolian.com/enterbj/platform/enterbj/maintPage.jsp";
            //System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            //httpClient.GetAsync(struri).ContinueWith((requestTask) => {
            //    System.Net.Http.HttpResponseMessage response = requestTask.Result;
            //});
            return View();
        }

        public IActionResult Order()
        {
            UserData data = null;
            var result = _apiClient.ApiPost<UserRequst, UserData>(new UserRequst() { userId = 100000 }, "/user/info");
            if (result.isSuccess)
            {
                data = result.data;
            }
            return View(data);
        }

        public IActionResult Articles()
        {
            return View();
        }
    }
}