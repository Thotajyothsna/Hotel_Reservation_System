//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
namespace HotelReservationSystem
{
    class Program
    {
        public List<Hotel> hotels = new List<Hotel>();
        public static void Main(string[] args)
        {
            DisplayMessage();

        }

        static void DisplayMessage()
        {
            Console.WriteLine("warm welcome to Hotel Reservation");
            Console.WriteLine("We provide clean and safe rooms to our customers");
        }
    }
}