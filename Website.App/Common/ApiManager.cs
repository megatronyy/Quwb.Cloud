using Lib.Framework.Core.IoC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.App.Common
{
    public class ApiManager
    {
        /// <summary>
        /// host地址
        /// </summary>
        static string ApiHost = IoCContainer.Resolve<IConfiguration>().GetSection("ApiManager").GetValue<string>("Host");
        static string AccessToken = IoCContainer.Resolve<IConfiguration>().GetSection("ApiManager").GetValue<string>("AccessToken");
        //商业类型接口
        public static string Api_GetBusinessTypeById = ApiHost + "/fang/business/{0}?accesstoken="+ AccessToken;

        public static string GetApiUrl(string api, params string[] para) {
            return string.Format(api, para);
        }
    }
}
