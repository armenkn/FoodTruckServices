using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class AuthenticatedUser
    {
        public int UserId { get; set; }
        public UserRoleEnum Role { get; set; }

    }
}
