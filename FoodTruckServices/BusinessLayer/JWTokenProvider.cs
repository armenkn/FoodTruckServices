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
        public string CreateToken(User user, string secret)
        {
            var header = new Header() { alg = Constants.Tokens.Algorithm, typ = Constants.Tokens.Type };
            var payload = new Payload() { UserId = user.UserId, UserRoleId = (int)user.UserRole, CreateDateTime = DateTime.Now, LifespanMins = Constants.Tokens.AccessTokenExpirationMinutes };

            var encodedHeader = Utilities.EncodeObject(header);
            var encodedPayload = Utilities.EncodeObject(payload);
            var encodedHeaderPayload = $"{encodedHeader}.{encodedPayload}";

            var hashString = ComputeHashStirng(encodedHeaderPayload, secret);

            return $"{encodedHeader}.{encodedPayload}.{hashString}";
        }

        public Tuple<AuthenticatedUser, TokenResponseEnum> ValidateToken(string token, string secret)
        {
            var jwtSections = token.Split('.').ToList();
            if (jwtSections.Count != 3)
            {
                return new Tuple<AuthenticatedUser, TokenResponseEnum>(null, TokenResponseEnum.InvalidToken);
            }

            var encodedHeader = jwtSections[0];
            var encodedPayload = jwtSections[1];
            var signature = jwtSections[2];

            var encodedHeaderPayload = $"{encodedHeader}.{encodedPayload}";

            if (encodedHeaderPayload != signature)
            {
                return new Tuple<AuthenticatedUser, TokenResponseEnum>(null, TokenResponseEnum.InvalidToken);
            }

            var originalPayload = Utilities.DecodeObject<Payload>(encodedPayload);

            if (originalPayload == null)
            {
                return new Tuple<AuthenticatedUser, TokenResponseEnum>(null, TokenResponseEnum.InvalidToken);
            }

            //TODO: check expiration date
            if (DateTime.Now > originalPayload.CreateDateTime.AddMinutes(originalPayload.LifespanMins))
            {
                return new Tuple<AuthenticatedUser, TokenResponseEnum>(null, TokenResponseEnum.TokenExpired);
            }

            return new Tuple<AuthenticatedUser, TokenResponseEnum>(new AuthenticatedUser()
            {
                Role = (UserRoleEnum)originalPayload.UserRoleId,
                UserId = originalPayload.UserId
            }, TokenResponseEnum.Success);
        }




        #region Private methods
        private string ComputeHashStirng(string encodedString, string secret)
        {
            var keyByte = Encoding.UTF8.GetBytes(secret);
            var hashString = "";

            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(encodedString));
                hashString = Convert.ToBase64String(hash);
            }

            return hashString;
        }
        #endregion
    }
}
