using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Website.ApiInvoke
{
    public class SerializerJsonHelper : ISerializerJsonHelper
    {
        public T JsonToObject<T>(string szJson)
        {
            var obj = Activator.CreateInstance<T>();
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        public string ObjectToJson(object obj)
        {
            if (obj == null) return string.Empty;
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
