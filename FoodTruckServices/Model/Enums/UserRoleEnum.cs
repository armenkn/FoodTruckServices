using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public enum UserRoleEnum
    {
        None = 0,
        Admin = 1,
        TruckDriver = 2,
        CompanyOwner = 3,
        AppUser = 4,
        Other = 100
    }
}
