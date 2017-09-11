using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public enum UserLoginResultEnum
    {
        None = 0,
        Success = 1,
        InvalidCredentials = 2, 
        NotFound = 3,
        UnableToLogin = 4
    }
}
