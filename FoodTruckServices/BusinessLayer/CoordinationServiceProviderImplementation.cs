using FoodTruckServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckServices.Model;

namespace FoodTruckServices
{
    public class CoordinationServiceProviderImplementation : ICoordinationServiceProvider
    {
        public CoordinationServiceProviderImplementation()
        {

        }

        public Coordination GetLatAndLongByAddress(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
