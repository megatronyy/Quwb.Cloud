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
        //商业类型接口
        public static string Api_GetBusinessTypeById = "/fang/business/{0}";
    }
}
