using System;
using System.Collections.Generic;
using System.Text;

namespace Website.ApiInvoke.Entity
{
    internal class ClientRequstHeader
    {
        /// <summary>
        /// 接入方应用的app key
        /// </summary>
        public string AppId { get; set; }

        public string Timestamp { get; set; }

        /// <summary>
        /// Api接口名称
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Api接口Sign
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// Api接口版本
        /// </summary>
        public string Version { get; set; }

        public SortedDictionary<string, string> ToSortedDictionary()
        {
            return new SortedDictionary<string, string>()
            {
                {"AppId", this.AppId},
                {"Timestamp", this.Timestamp},
                {"Method", this.Method},
                {"Version", this.Version}
            };
        }

        public string ToRequestParamsString()
        {
            return $"AppId={AppId}" + "&" + $"Timestamp={Timestamp}" + "&" + $"Method={Method}" + "&" + $"Sign={Sign}" +
                   "&" + $"Version={Version}";
        }

    }
}
