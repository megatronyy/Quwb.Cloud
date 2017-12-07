using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Website.ApiInvoke
{
    public class HttpClient
    {
        public static string HttpGet(string Url, string getDataStr, int timeOut = 3000)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url + ((getDataStr == "") ? "" : "?") + getDataStr);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "text/html;charset=UTF-8";
            httpWebRequest.Proxy = null;
            httpWebRequest.Timeout = timeOut;
            string result;
            using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                result = text;
            }
            return result;
        }
        public static string HttpPost(string posturl, string postData, int timeOut = 3000)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] bytes = encoding.GetBytes(postData);
            string result;
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json;charset=UTF-8";
                httpWebRequest.ContentLength = (long)bytes.Length;
                httpWebRequest.Proxy = null;
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse httpWebResponse2;
                HttpWebResponse httpWebResponse = httpWebResponse2 = (httpWebRequest.GetResponse() as HttpWebResponse);
                try
                {
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream, encoding);
                    string text = streamReader.ReadToEnd();
                    string empty = string.Empty;
                    result = text;
                }
                finally
                {
                    if (httpWebResponse2 != null)
                    {
                        ((IDisposable)httpWebResponse2).Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = string.Empty;
            }
            return result;
        }
        public static HttpWebResponse HttpPostMuiltThread(string url, Encoding encoding = null, int timeout = 1000, string method = "GET", string postParameter = "")
        {
            bool flag = encoding == null;
            if (flag)
            {
                encoding = Encoding.Default;
            }
            ServicePointManager.DefaultConnectionLimit = 512;
            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Proxy = null;
            webRequest.Timeout = timeout;
            webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");
            bool flag2 = method.ToUpper() == "POST";
            if (flag2)
            {
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                bool flag3 = !string.IsNullOrEmpty(postParameter);
                if (flag3)
                {
                    byte[] bytes = encoding.GetBytes(postParameter);
                    webRequest.ContentLength = (long)bytes.Length;
                    using (Stream requestStream = webRequest.GetRequestStream())
                    {
                        requestStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            else
            {
                webRequest.Method = "GET";
            }
            return (HttpWebResponse)webRequest.GetResponse();
        }
    }
}
