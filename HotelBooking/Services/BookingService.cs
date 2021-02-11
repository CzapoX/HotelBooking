using HotelBooking.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Services
{
    public interface IBookingService
    {
        void BeginBooking(List<Hotel> hotelsCached, Reservation reservation);
    }

    public class BookingService : IBookingService
    {
        private readonly IConsoleService consoleService;

        public BookingService(IConsoleService consoleService)
        {
            this.consoleService = consoleService;
        }

        public void BeginBooking(List<Hotel> hotels, Reservation reservation)
        {
            Console.WriteLine("ID; Nazwa;         Lokalizacja; Cena za pokój na jedną noc, na osobę");
            foreach (var hotel in hotels)
            {
                Console.WriteLine($"{hotel.Id};  {hotel.Name}; {hotel.Location};        {hotel.PriceForOnePerson};");
            }

            int chosenHotelId = GetChoosenHotelIdFromUser();

            while (!hotels.Any(x => x.Id == chosenHotelId))
            {
                consoleService.WriteToConsole("Podane Id hotelu nie istnieje");
                chosenHotelId = GetChoosenHotelIdFromUser();
            }

            var chosenHotel = hotels.FirstOrDefault(x => x.Id == chosenHotelId);

            reservation.HotelId = chosenHotelId;
            reservation.Date = GetChosenReservationDateFromUser();
            reservation.HowManyDays = GetNumberOfDaysFromUser();
            reservation.NumberOfPeople = GetNumberOfPeopleFromUser();
            reservation.ReservationNumber = Guid.NewGuid();
            reservation.BasePrice = chosenHotel.PriceForOnePerson;
            reservation.PriceToPay = reservation.NumberOfPeople * reservation.BasePrice * reservation.HowManyDays;
            reservation.IsBookingSuccessful = true;
            reservation.IsReservationSuccessful = true;
            reservation.IsEmailSendSuccessful = false;
            reservation.IsPaymentSuccessful = false;
            reservation.IsPaymentNecessary = chosenHotel.IsPaymentNecessary;
        }

        private int GetNumberOfPeopleFromUser()
        {
            consoleService.WriteToConsole("Podaj dla ilu osób rezerwowane jest miejsce w hotelu");
            var result = consoleService.GetNumberFromUser();

            return result;
        }

        private int GetNumberOfDaysFromUser()
        {
            consoleService.WriteToConsole("Podaj ile dni ma trwać pobyt");
            var result = consoleService.GetNumberFromUser();

            return result;
        }

        public DateTime GetChosenReservationDateFromUser()
        {
            consoleService.WriteToConsole("Podaj dzień który chcesz zarezerwować");
            var reservationDate = consoleService.GetDateTimeFromUser();

            return reservationDate;
        }

        public int GetChoosenHotelIdFromUser()
        {
            consoleService.WriteToConsole("Podaj Id hotelu w którym chciałbyś zarezerwować pokój");
            var result = consoleService.GetNumberFromUser();

            return result;
        }
    }
}
