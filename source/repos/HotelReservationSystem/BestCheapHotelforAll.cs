using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Hotel
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

class HotelBookingSystem
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
        if (checkinDate >= checkoutDate)
        {
            throw new ArgumentException("Checkout date must be after the check-in date.");
        }

        if (checkinDate.Date < DateTime.Today || checkoutDate.Date < DateTime.Today)
        {
            throw new ArgumentException("Check-in and checkout dates cannot be in the past.");
        }

        int weekdayCount = (int)(checkoutDate - checkinDate).TotalDays - Enumerable.Range(0, (int)(checkoutDate - checkinDate).TotalDays)
            .Count(i => checkinDate.AddDays(i).DayOfWeek == DayOfWeek.Saturday || checkinDate.AddDays(i).DayOfWeek == DayOfWeek.Sunday);
        int weekendCount = (int)(checkoutDate - checkinDate).TotalDays - weekdayCount;

        if (weekdayCount < 0 || weekendCount < 0)
        {
            throw new ArgumentException("Invalid date range. Please provide valid dates.");
        }

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

        try
        {
            DateTime checkinDate = GetValidDate("Enter check-in date (e.g., 11Sep2020 or YYYY-MM-DD): ");
            DateTime checkoutDate = GetValidDate("Enter checkout date (e.g., 11Sep2020 or YYYY-MM-DD): ");

            bool isRewardsCustomer = GetValidCustomerType();

            Hotel cheapestHotel = bookingSystem.FindCheapestHotel(checkinDate, checkoutDate, isRewardsCustomer);

            Console.WriteLine("Cheapest hotel for the given date range is: " + cheapestHotel.Name);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static DateTime GetValidDate(string message)
    {
        DateTime date;
        bool isValidDate;

        do
        {
            Console.Write(message);
            string inputDate = Console.ReadLine().Trim();

            // Try parsing date in "ddMMMyyyy" format (e.g., 11Sep2020)
            isValidDate = DateTime.TryParseExact(inputDate, "ddMMMyyyy", null, System.Globalization.DateTimeStyles.None, out date);

            // If parsing fails, try parsing date in "YYYY-MM-DD" format
            if (!isValidDate)
            {
                isValidDate = DateTime.TryParse(inputDate, out date);
            }

            if (!isValidDate)
            {
                Console.WriteLine("Invalid date format. Please enter a valid date in either '11Sep2020' or 'YYYY-MM-DD' format.");
            }
        } while (!isValidDate);

        return date;
    }

    static bool GetValidCustomerType()
    {
        string input;
        do
        {
            Console.Write("Are you a rewards customer? (yes/no): ");
            input = Console.ReadLine().Trim().ToLower();

            if (input != "yes" && input != "no")
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
            }
        } while (input != "yes" && input != "no");

        return input == "yes";
    }
}
