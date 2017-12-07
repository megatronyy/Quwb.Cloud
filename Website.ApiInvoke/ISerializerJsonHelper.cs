using System;
using System.Collections.Generic;
namespace Website.ApiInvoke
{
    public interface ISerializerJsonHelper
    {
        string ObjectToJson(object obj);

        T JsonToObject<T>(string szJson);
    }
}
