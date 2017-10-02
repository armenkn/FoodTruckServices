using FoodTruckServices.Model;
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

        public static string ToAddressString(this Address address)
        {
            var result = address.Address1.Replace(' ', '+') + "+";
            result += address.Address2?.Replace(' ', '+') + "+";
            result += address.City.Replace(' ', '+') + "+";
            result += address.State.Replace(' ', '+') + "+";
            result += address.Zipcode;

            return result;
        }

        public static string EncodeObject<T>(this T originalObject)
        {
            var encodedObjectBytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(originalObject));
            var encodedObject = System.Convert.ToBase64String(encodedObjectBytes);
            return encodedObject;
        }

        public static T DecodeObject<T>(string encodedObject)
        {
            var convertedFromBase64 = Convert.FromBase64CharArray(encodedObject.ToCharArray(), 0, encodedObject.Length);
            var serializedObject = System.Text.Encoding.UTF8.GetString(convertedFromBase64);
            T originalObject = JsonConvert.DeserializeObject<T>(serializedObject);
            return originalObject;
        }
    }
}

