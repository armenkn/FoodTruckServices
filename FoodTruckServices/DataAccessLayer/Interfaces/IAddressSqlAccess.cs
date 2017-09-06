using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IAddressSqlAccess
    {
        Address GetAddressById(int id);
        int CreateAddress(Address address);
        void UpdateAddress(Address address);

    }
}
