using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class Location
    {
        public int LocationID { get; set; }

        public Address Address { get; set; }

        public virtual List<Phone> PhoneNumbers { get; set; }

        public virtual List<WorkingDayHour> WorkingDayHours { get; set; }

    }
}
