using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem
{
    public class Hotel
    {
        public string Hotel_Name;
        public int Hotel_Ratings;
        public int Weekday_Rates;
        public int Weekend_Rates;
        public Hotel(string hotel_name,int hotel_rating,int weekday_rates,int weekend_rates)
        { 
            Hotel_Name = hotel_name;
            Hotel_Ratings = hotel_rating;
            Weekday_Rates = weekday_rates;
            Weekend_Rates = weekend_rates;
            
        }
        
    }
}
