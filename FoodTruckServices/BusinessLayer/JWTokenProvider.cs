using FoodTruckServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;

namespace FoodTruckServices.BusinessLayer
{
    public class JWTokenProvider : ITokenProvider
    {
        public string CreateToken(string algorithm, string type, User user, string secret)
        {
            var header = new Header() { alg = algorithm, typ = type };
            var payload = new Payload() { Username = user.Username, UserRoleId = ((int)user.UserRole).ToString() };

            var encodedHeaderBytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header));
            var encodedHeader = System.Convert.ToBase64String(encodedHeaderBytes);

            var encodedPayloadBytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload));
            var encodedPayload = System.Convert.ToBase64String(encodedPayloadBytes);

            var encodedHeaderPayload = $"{encodedHeader}.{encodedPayload}";

            var keyByte = Encoding.UTF8.GetBytes(secret);
            var hashString = "";

            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(encodedHeaderPayload));
                hashString = Convert.ToBase64String(hash);
            }

            return $"{encodedHeader}.{encodedPayload}.{hashString}";
        }
    }
}
