using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem
{

    public class Hotel
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public int WeekdayRateRegular { get; set; }
        public int WeekendRateRegular { get; set; }
        public int WeekdayRateRewards { get; set; }
        public int WeekendRateRewards { get; set; }

        public Hotel(string name, int rating, int weekdayRateRegular, int weekendRateRegular, int weekdayRateRewards, int weekendRateRewards)
        {
            Name = name;
            Rating = rating;
            WeekdayRateRegular = weekdayRateRegular;
            WeekendRateRegular = weekendRateRegular;
            WeekdayRateRewards = weekdayRateRewards;
            WeekendRateRewards = weekendRateRewards;
        }
    }


}
