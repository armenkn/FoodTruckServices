using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class WorkingDayHourString
    {
        public int WorkingDayHourID { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public int OpenTimeHours { get; set; }

        public int OpenTimeMinutes { get; set; }

        public int CloseTimeHours { get; set; }

        public int CloseTimeMinutes { get; set; }
    }
}
