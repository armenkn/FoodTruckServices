using System;

namespace FoodTruckServices.Model
{
    public class WorkingDayHour
    {
        public int WorkingDayHourID { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan OpenTime { get; set; }

        public TimeSpan CloseTime { get; set; }

    }
}