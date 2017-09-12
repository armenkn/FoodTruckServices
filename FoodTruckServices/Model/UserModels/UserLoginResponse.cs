using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class UserLoginResponse
    {
        public int UserId { get; set; }
        public UserLoginResultEnum LoginResult { get; set; }
        public string JWT { get; set; }
    }
}
