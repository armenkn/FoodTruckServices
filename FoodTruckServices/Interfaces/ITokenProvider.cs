using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Interfaces
{
    public interface ITokenProvider
    {
        string CreateToken(string algorithm, string type, User user, string secret);
    }
}
