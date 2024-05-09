using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem
{
    class BestCheapHotel
    {
        private List<Hotel> hotels;

        public BestCheapHotel(List<Hotel> hotels)
        {
            this.hotels = hotels;
        }

        public int CalculateTotalCost(Hotel hotel, int weekdayCount, int weekendCount)
        {
            return (weekdayCount * hotel.Weekday_Rates) + (weekendCount * hotel.Weekend_Rates);
        }

        public Hotel FindCheapestHotel(DateTime checkinDate, DateTime checkoutDate)
        {
            int weekdayCount = 0;
            int weekendCount = 0;
            for (DateTime date = checkinDate; date < checkoutDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    weekendCount++;
                else
                    weekdayCount++;
            }

            return hotels
                .OrderBy(hotel => CalculateTotalCost(hotel, weekdayCount, weekendCount))
                .First();
        }
    }

}
