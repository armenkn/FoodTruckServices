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
        int CreateAddress(Address address, int userId);
        void UpdateAddress(Address address, int userId);
        DatabaseResponse DeleteAddress(int id, int userId);
    }
}
