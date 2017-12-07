using Lib.Framework.Core.IoC;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Website.ApiInvoke.Config
{
    public class ApiConfig
    {
        private IConfigurationSection section;
        public ApiConfig(string sectionName)
        {
            section = IoCContainer.Resolve<IConfiguration>().GetSection(sectionName);
        }

        /// <summary>
        /// Api调用地址
        /// </summary>
        public string ApiUrl
        {
            get { return section.GetValue<string>("ApiUrl"); }
        }

        /// <summary>
        /// 应用Id
        /// </summary>
        public string AppId
        {
            get { return section.GetValue<string>("AppId"); }
        }

        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppKey
        {
            get { return section.GetValue<string>("AppKey"); }
        }

        /// <summary>
        /// 接口版本
        /// </summary>
        public string Version
        {
            get { return section.GetValue<string>("Version"); }
        }

        /// <summary>
        /// 是否启用返回值验签
        /// </summary>
        public bool IfResponseValidateSign
        {
            get { return section.GetValue<bool>("IfResponseValidateSign"); }
        }

        /// <summary>
        /// 是否启用全参数验签
        /// </summary>
        public bool IsFullParamSign
        {
            get { return (bool)section.GetValue<bool>("IsFullParamSign"); }
        }
    }
}
