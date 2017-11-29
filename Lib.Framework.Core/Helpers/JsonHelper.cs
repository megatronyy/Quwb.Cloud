using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Framework.Core.Helpers
{
    public class JsonHelper
    {
        public static T JsonTo<T>(string requestStr)
        {
            try
            {
                T obj = JsonConvert.DeserializeObject<T>(requestStr);
                return obj;

            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
