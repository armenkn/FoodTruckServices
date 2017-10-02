using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Interfaces
{
    public interface ITokenProvider
    {
        string CreateToken(User user, string secret);

        Tuple<AuthenticatedUser, TokenResponseEnum> ValidateToken(string token, string secret);
    }
}
