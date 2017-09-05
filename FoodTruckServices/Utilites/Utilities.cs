using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FoodTruckServices
{
    public static class Utilities
    {
        public static string GetDefaultConnectionString()
        {
            return Startup.ConnectionString;
        }

        public static string SerializeObjectToJson<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
        
        public static string SerializeObjectToXml<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }

        }

        public static T DeserializeXmlToObject<T>(string serializedXml)
        {
            T result;

            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(serializedXml))
            {
                result = (T)xmlSerializer.Deserialize(reader);
            }

            return result;
        }
    }
}
