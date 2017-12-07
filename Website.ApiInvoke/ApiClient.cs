using Lib.Framework.Core.IoC;
using log4net.Core;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Website.ApiInvoke.Config;
using Website.ApiInvoke.Entity;

namespace Website.ApiInvoke
{
    public class ApiClient
    {
        private readonly ApiConfig _apiConfig;  

        public ApiClient(string configSectionName)
        {
            try
            {
                _apiConfig = new ApiConfig(configSectionName);
            }
            catch (Exception exception)
            {
                throw new ArgumentException("Api配置文件读取错误", exception);
            }
        }

        public ApiClient(ApiConfig config)
        {
            if (config == null) throw new ArgumentException("Api配置文件不能为空");
            _apiConfig = config;
        }

        /// <summary>
        /// 调用Api获取返回结果
        /// </summary>
        /// <param name="requestData">Post数据</param>
        /// <param name="method">调用方法名</param>
        /// <returns></returns>
        public ResponseEntity<TResponseData> ApiPost<TRequestData, TResponseData>(TRequestData requestData,
            string method, Func<TRequestData, string> requestFunc = null, Func<string, ResponseEntity<TResponseData>> func = null)
            where TRequestData : new() where TResponseData : new()
        {
            try
            {
                var validateResult = ValidateConfig<TResponseData>(method);
                if (!validateResult.isSuccess) return validateResult;
                var postData = requestFunc != null ? requestFunc(requestData) : new SerializerJsonHelper().ObjectToJson(requestData);
                var response = HttpClient.HttpPost(GetPostUrl(method), postData, 3000);
                return func != null ? func(response) : new SerializerJsonHelper().JsonToObject<ResponseEntity<TResponseData>>(response);
            }
            catch (Exception exception)
            {
                var result = CreateDefaultRequest<TResponseData>();
                result.message = exception.Message;
                return result;
            }
        }
 

        /// <summary>
        /// 校验配置文件
        /// </summary>
        /// <returns></returns>
        private ResponseEntity<TResponseData> ValidateConfig<TResponseData>(string method) where TResponseData : new()
        {
            var result = CreateDefaultRequest<TResponseData>();
            if (string.IsNullOrWhiteSpace(_apiConfig.AppId))
                result.message = "配置文件中AppId不能为空";
            if (string.IsNullOrWhiteSpace(_apiConfig.ApiUrl))
                result.message = "配置文件中ApiUrl不能为空";
            if (string.IsNullOrWhiteSpace(_apiConfig.AppKey))
                result.message = "配置文件中AppKey不能为空";
            if (string.IsNullOrWhiteSpace(_apiConfig.Version))
                result.message = "配置文件中Version不能为空";
            if (string.IsNullOrWhiteSpace(method))
                result.message = "方法名参数Method不能为空";
            result.isSuccess = true;
            return result;
        }

        private string GetPostUrl(string method)
        {
            var header = new ClientRequstHeader()
            {
                AppId = _apiConfig.AppId,
                //Method = method,
                Timestamp = DateTime.Now.ToString("yyyyMMddhhmmssms"),
                Version = _apiConfig.Version
            };
            //var signClient = new SignClient(_apiConfig.AppKey, Encoding.UTF8);
            //var signStr = signClient.GetSign(header.ToSortedDictionary());
            //header.Sign = signStr;
            var postUrl = _apiConfig.ApiUrl + method + "?" + header.ToRequestParamsString();
            
            return postUrl;
        }

        public string ToReqeustParamString<T>(T data) where T : new()
        {
            var parms = (from info in data.GetType().GetProperties()
                         let paramsName = info.Name
                         select $"{paramsName}={info.GetValue(data, null)}").ToList();

            return string.Join("&", parms);
        }

        private ResponseEntity<TResponseData> CreateDefaultRequest<TResponseData>()
            where TResponseData : new()
        {
            return new ResponseEntity<TResponseData>()
            {
                isSuccess = false,
                message = "",
                data = default(TResponseData)
            };
        }
    }
}
