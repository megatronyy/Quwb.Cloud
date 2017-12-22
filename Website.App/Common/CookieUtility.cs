using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.App.Common
{
    public class CookieUtility
    {
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strName].Value);
                //return HttpContext.Current.Request.Cookies[strName].Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="val">cookie value</param>
        /// <param name="domain">cookie域</param>
        /// <param name="expiredTime">cookie过期时间</param>
        /// <param name="httpOnly">不允许客户端访问cookie</param>
        private static void SetCookiePrivate(string name, string val, string domain, DateTime? expiredTime, bool? httpOnly)
        {
            var cookie = new HttpCookie(name);
            if (!string.IsNullOrEmpty(domain))
                cookie.Domain = domain;

            if (expiredTime != null)
                cookie.Expires = expiredTime.Value;

            if (httpOnly != null)
                cookie.HttpOnly = httpOnly.Value;
            cookie.Value = HttpUtility.UrlEncode(val, Encoding.GetEncoding("utf-8"));
            if (HttpContext.Current.Request.Cookies[name] == null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                HttpContext.Current.Request.Cookies[name].Value = val;
                HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(-100);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="domain">cookie域</param>
        /// <param name="val">cookie value</param>
        /// <param name="expiredTime">cookie过期时间</param>
        /// <param name="httpOnly">不允许客户端访问cookie</param>
        public static void SetCookie(string name, string domain, string val, DateTime? expiredTime, bool? httpOnly)
        {
            SetCookiePrivate(name, val, domain, expiredTime, httpOnly);
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="val">cookie value</param>
        /// <param name="expiredTime">cookie过期时间</param>
        /// <param name="httpOnly">不允许客户端访问cookie</param>
        public static void SetCookie(string name, string val, DateTime expiredTime, bool httpOnly)
        {
            SetCookiePrivate(name, val, string.Empty, expiredTime, httpOnly);
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="val">cookie value</param>
        /// <param name="httpOnly">不允许客户端访问</param>
        public static void SetCookie(string name, string val, bool httpOnly)
        {
            SetCookiePrivate(name, val, string.Empty, null, httpOnly);
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">cookie name</param>
        /// <param name="val">cookie val</param>
        public static void SetCookie(string name, string val)
        {
            SetCookiePrivate(name, val, string.Empty, null, null);
        }

        /// <summary>
        /// 移除cookie
        /// </summary>
        /// <param name="name">cookie name</param>
        public static void RemoveCookie(string name)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(name) { Value = string.Empty, Expires = DateTime.Now.AddDays(-1) });
        }
    }
}
