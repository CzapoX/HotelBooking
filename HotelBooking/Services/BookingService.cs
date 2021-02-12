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

            var chosenHotel = GetChosenHotelFromUser(hotels);

            reservation.ReservationNumber = Guid.NewGuid();
            reservation.HotelId = chosenHotel.Id;
            reservation.BasePrice = chosenHotel.PriceForOnePerson;
            reservation.IsPaymentNecessary = chosenHotel.IsPaymentNecessary;
            reservation.Date = GetChosenReservationDateFromUser();
            reservation.HowManyDays = GetNumberOfDaysFromUser();
            reservation.NumberOfPeople = GetNumberOfPeopleFromUser();
            reservation.PriceToPay = reservation.NumberOfPeople * reservation.BasePrice * reservation.HowManyDays;
            reservation.IsBookingSuccessful = true;
            reservation.IsReservationSuccessful = true;
            reservation.IsEmailSendSuccessful = false;
            reservation.IsPaymentSuccessful = false;
        }

        private int GetNumberOfPeopleFromUser()
        {
            consoleService.WriteToConsole("Podaj dla ilu osób rezerwowane jest miejsce w hotelu");
            var result = consoleService.GetNumberFromUser();

            return result;
        }

        public int GetNumberOfDaysFromUser()
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

        public Hotel GetChosenHotelFromUser(List<Hotel> availableHotels)
        {
            consoleService.WriteToConsole("Podaj Id hotelu w którym chciałbyś zarezerwować pokój");
            var chosenHotelId = consoleService.GetNumberFromUser();

            while (!availableHotels.Any(x => x.Id == chosenHotelId))
            {
                consoleService.WriteToConsole("Podane Id hotelu nie istnieje");
                chosenHotelId = consoleService.GetNumberFromUser();
            }

            return availableHotels.FirstOrDefault(x => x.Id == chosenHotelId);
        }
    }
}
