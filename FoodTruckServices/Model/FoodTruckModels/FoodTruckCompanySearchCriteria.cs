using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class FoodTruckCompanySearchCriteria
    {
        public int Id { get; set; }

        public string BusinessName { get; set; }

        public string Zipcode { get; set; }

        public string City{ get; set; }

        public AreaCoordination Coordination { get; set; }
    }
}
