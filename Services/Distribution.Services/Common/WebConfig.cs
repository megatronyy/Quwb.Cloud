using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribution.Services.Common
{
    public class WebConfig
    {
        public static string ShopHouseRW
        {
            get { return GetConnectionStrings("ShopHouseRW"); }
        }

        public static string GetConnectionStrings(string key)
        {
            string reValue = "";
            if (ConfigurationManager.ConnectionStrings[key] != null)
            {
                reValue = ConfigurationManager.ConnectionStrings[key].ToString();
            }
            return reValue;
        }
    }
}
