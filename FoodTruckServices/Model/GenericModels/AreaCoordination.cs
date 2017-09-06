using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class AreaCoordination
    {
        public Coordination Coordination { get; set; }
        public float Radius { get; set; }

        public DistanceUnit Unit { get; set; }
    }
}
