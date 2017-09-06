using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Interfaces
{
    public interface ICoordinationServiceProvider
    {
        Coordination GetLatAndLongByAddress(Address address);
    }
}
