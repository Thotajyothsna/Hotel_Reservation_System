using System;
using System.Collections.Generic;
using System.Linq;

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

public class HotelBookingSystem
{
    private List<Hotel> hotels;

    public HotelBookingSystem(List<Hotel> hotels)
    {
        this.hotels = hotels;
    }

    public int CalculateTotalCost(Hotel hotel, int weekdayCount, int weekendCount, bool isRewardsCustomer)
    {
        int weekdayRate = isRewardsCustomer ? hotel.WeekdayRateRewards : hotel.WeekdayRateRegular;
        int weekendRate = isRewardsCustomer ? hotel.WeekendRateRewards : hotel.WeekendRateRegular;
        return (weekdayCount * weekdayRate) + (weekendCount * weekendRate);
    }

    public Hotel FindCheapestHotel(DateTime checkinDate, DateTime checkoutDate, bool isRewardsCustomer)
    {
        int weekdayCount = (int)(checkoutDate - checkinDate).TotalDays - Enumerable.Range(0, (int)(checkoutDate - checkinDate).TotalDays)
            .Count(i => checkinDate.AddDays(i).DayOfWeek == DayOfWeek.Saturday || checkinDate.AddDays(i).DayOfWeek == DayOfWeek.Sunday);
        int weekendCount = (int)(checkoutDate - checkinDate).TotalDays - weekdayCount;

        Hotel cheapestHotel = hotels
            .OrderBy(hotel => CalculateTotalCost(hotel, weekdayCount, weekendCount, isRewardsCustomer))
            .ThenByDescending(hotel => hotel.Rating) // If costs are the same, prioritize higher rating
            .First();

        return cheapestHotel;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Hotel> hotels = new List<Hotel>
        {
            new Hotel("Lakewood", 3, 110, 90, 80, 80),
            new Hotel("Bridgewood", 4, 160, 60, 110, 50),
            new Hotel("Ridgewood", 5, 220, 150, 100, 40)
        };

        HotelBookingSystem bookingSystem = new HotelBookingSystem(hotels);

        DateTime checkinDate = new DateTime(2024, 5, 15);
        DateTime checkoutDate = new DateTime(2024, 5, 18);
        bool isRewardsCustomer = true; // Set to true if the customer is a rewards member

        Hotel cheapestHotel = bookingSystem.FindCheapestHotel(checkinDate, checkoutDate, isRewardsCustomer);

        Console.WriteLine("Cheapest hotel for the given date range is: " + cheapestHotel.Name);
    }
}
