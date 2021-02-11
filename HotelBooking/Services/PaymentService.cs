using HotelBooking.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace HotelBooking.Services
{
    public interface IPaymentService
    {
        void BeginPayment(Reservation reservation);
    }

    class PaymentService : IPaymentService
    {
        private readonly HotelDbContext dbContext;
        private readonly IConsoleService consoleService;
        private readonly ILogger<PaymentService> logger;

        public PaymentService(HotelDbContext dbContext, IConsoleService consoleService, ILogger<PaymentService> logger)
        {
            this.dbContext = dbContext;
            this.consoleService = consoleService;
            this.logger = logger;
        }

        public void BeginPayment(Reservation reservation)
        {
            var chosenHotel = dbContext.Hotels.FirstOrDefault(x => x.Id == reservation.HotelId);
            if (chosenHotel.IsPaymentNecessary == false)
            {
                consoleService.WriteToConsole
                    ($"Hotel nie wymaga zapłaty w trakcie rezerwacji, cena do zapłaty to {reservation.PriceToPay}");
                consoleService.WriteToConsole("Czy chcesz zapłacić teraz? (Y/N)");
                if (consoleService.GetBoolFromUser() == false)
                {
                    reservation.IsPaymentSuccessful = false;
                    return;
                }
            }
            CheckIfPrizeIsStillCorrect(reservation);

            consoleService.WriteToConsole($"Do zapłaty ${reservation.PriceToPay}");

            consoleService.WriteToConsole
                ("Rozpoczynamy transakcję, prosimy podać numer karty kredtyowej (dowolne 4 cyfry)");
            int creditCardNumber = consoleService.GetCreditCardFromUser();

            try
            {
                //TODO Integration with 3rd party payment
                reservation.IsPaymentSuccessful = true;
                consoleService.WriteToConsole("Płatność powiodła się");
            }
            catch (Exception ex)
            {
                reservation.IsPaymentSuccessful = false;
                if (chosenHotel.IsPaymentNecessary)
                    reservation.IsReservationSuccessful = false;
                consoleService.WriteToConsole("Wystąpił błąd w trakcje transakcji");
                logger.LogError(ex, $"Error during payment for {reservation.ReservationNumber}");
            }
        }

        private void CheckIfPrizeIsStillCorrect(Reservation reservation)
        {
            var priceFromDb = dbContext.Hotels.FirstOrDefault(x => x.Id == reservation.HotelId).PriceForOnePerson;
            if (priceFromDb != reservation.BasePrice)
            {
                consoleService.WriteToConsole($"Cena rezerwacji uległa zmianie, nowa cena za noc, na jedną osobę to: {priceFromDb}");
                consoleService.WriteToConsole("Czy chcesz kontynować rezerwację? (Y/N)");

                if (consoleService.GetBoolFromUser() == false)
                {
                    reservation.IsPaymentSuccessful = false;
                    reservation.IsReservationSuccessful = false;
                }
                else
                {
                    reservation.PriceToPay = priceFromDb * reservation.NumberOfPeople;
                }
            }
        }
    }
}
