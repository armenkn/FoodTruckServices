using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class FoodTruckSearchCriteria
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DirverName { get; set; }
        
        public AreaCoordination Coordination { get; set; }
    }
}
