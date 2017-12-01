using Lib.Framework.Core.IoC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.App.Common
{
    public sealed class WebConstant
    {
        private static IConfigurationSection _section = IoCContainer.Resolve<IConfiguration>().GetSection("WebConstant");

        public static string WEBNAME = _section.GetValue<string>("WebName");
    }
}
