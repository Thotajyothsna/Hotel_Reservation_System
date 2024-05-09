using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem
{
    public class Hotel
    {
        public string Hotel_name;
        public int Hotel_Rating;
        public Hotel(string hotel_name,int hotel_rating)
        { 
            Hotel_name = hotel_name;
            Hotel_Rating = hotel_rating;
        }
    }
}
