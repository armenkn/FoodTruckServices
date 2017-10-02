using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public enum TokenResponseEnum
    {
        None = 0,
        Success = 1,
        TokenExpired = 2,
        InvalidToken = 3,
        Other = 100
    }
}
