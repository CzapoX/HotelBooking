using HotelBooking.Data;
using System;
using System.Linq;

namespace HotelBooking.Services
{
    class PaymentService : IPaymentService
    {
        private readonly HotelDbContext dbContext;
        private readonly IConsoleService consoleService;

        public PaymentService(HotelDbContext dbContext, IConsoleService consoleService)
        {
            this.dbContext = dbContext;
            this.consoleService = consoleService;
        }

        public void BeginPayment(Reservation reservation)
        {
            CheckIfPrizeIsStillCorrect(reservation);
        }

        private void CheckIfPrizeIsStillCorrect(Reservation reservation)
        {
            var priceFromDb = dbContext.Hotels.FirstOrDefault(x => x.Id == reservation.HotelId).PriceForOnePerson;
            if (priceFromDb != reservation.Hotel.PriceForOnePerson)
            {
                consoleService.WriteToConsole($"Cena rezerwacji uległa zmianie, nowa cena za noc, na jedną osobę to: {priceFromDb}");
                consoleService.WriteToConsole("Czy chcesz kontynować rezerwację? (Y/N)");

                if (consoleService.GetBoolFromUser() == false)
                {
                    reservation.IsPaymentSuccessful = false;
                    reservation.IsReservationSuccessful = false;
                }
            }
        }
    }

    public interface IPaymentService
    {
        void BeginPayment(Reservation reservation);
    }
}
