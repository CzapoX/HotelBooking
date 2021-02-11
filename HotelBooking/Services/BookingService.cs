using HotelBooking.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Services
{
    class BookingService : IBookingService
    {
        public void Run(List<Hotel> hotels, Reservation reservation)
        {
            Console.WriteLine("ID; Nazwa;         Lokalizacja; Cena za pokój na jedną noc, na osobę");
            foreach (var hotel in hotels)
            {
                Console.WriteLine($"{hotel.Id};  {hotel.Name}; {hotel.Location};        {hotel.PriceForOnePerson};");
            }

            int chosenHotelId = GetChoosenHotelIdFromUser();

            while (!hotels.Any(x => x.Id == chosenHotelId))
            {
                Console.WriteLine();
                Console.WriteLine("Podane Id hotelu nie istnieje");
                chosenHotelId = GetChoosenHotelIdFromUser();
            }

            reservation.HotelId = chosenHotelId;

            reservation.Date = GetChosenReservationDateFromUser();

            reservation.HowManyDays = GetNumberOfDaysFromUser();

            reservation.NumberOfPeople = GetNumberOfPeopleFromUser();
            reservation.ReservationNumber = Guid.NewGuid();

            reservation.IsBookingSuccessful = true;
        }

        public int GetNumberOfPeopleFromUser()
        {
            Console.WriteLine();
            Console.WriteLine("Podaj dla ilu osób rezerwowane jest miejsce w hotelu");
            int result;
            if (ValidateNumberFromConsole(out result))
            {
                Console.WriteLine();
                Console.WriteLine("Podana wartość jest niepoprawna");
                GetNumberOfPeopleFromUser();
            }

            return result;
        }

        private bool ValidateNumberFromConsole(out int result)
        {
            return int.TryParse(Console.ReadLine(), out result) == false || result < 1;
        }

        private int GetNumberOfDaysFromUser()
        {
            Console.WriteLine();
            Console.WriteLine("Podaj ile dni ma trwać pobyt");
            if (ValidateNumber(out result))
            {
                Console.WriteLine();
                Console.WriteLine("Podana wartość jest niepoprawna");
                GetNumberOfDaysFromUser();
            }

            return result;
        }

        private DateTime GetChosenReservationDateFromUser()
        {
            Console.WriteLine();
            Console.WriteLine("Podaj dzień który chcesz zarezerwować");
            
            if (DateTime.TryParse(Console.ReadLine(), out DateTime reservationDate) == false)
            {
                Console.WriteLine("Podałeś nieprawidłową datę");
                GetChosenReservationDateFromUser();
            }

            if (reservationDate < DateTime.Now)
            {
                Console.WriteLine("Podałeś za wczesną datę");
                GetChosenReservationDateFromUser();
            }
            
            return reservationDate;
        }

        private int GetChoosenHotelIdFromUser()
        {
            Console.WriteLine();
            Console.WriteLine("Podaj Id hotelu w którym chciałbyś zarezerwować pokój");
            
            if (int.TryParse(Console.ReadLine(), out int result) == false)
            {
                Console.WriteLine();
                Console.WriteLine("Podana wartość jest niepoprawna");
                GetChoosenHotelIdFromUser();
            }

            return result;
        }
    }

    internal interface IBookingService
    {
        void Run(List<Hotel> hotelsCached, Reservation reservation);
    }
}
