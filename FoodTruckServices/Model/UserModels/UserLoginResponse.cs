using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model.UserModels
{
    public class UserLoginResponse
    {
        public User User { get; set; }
        public UserLoginResultEnum LoginResult { get; set; }
        public string JWT { get; set; }

    }
}
