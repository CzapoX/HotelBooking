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

    public class PaymentService : IPaymentService
    {
        private readonly IConsoleService consoleService;
        private readonly ILogger<PaymentService> logger;

        public PaymentService(IConsoleService consoleService, ILogger<PaymentService> logger)
        {
            this.consoleService = consoleService;
            this.logger = logger;
        }

        public void BeginPayment(Reservation reservation)
        {
            if (reservation.IsPaymentNecessary == false)
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

            consoleService.WriteToConsole($"Do zapłaty {reservation.PriceToPay}");

            consoleService.WriteToConsole
                ("Rozpoczynamy transakcję, prosimy podać numer karty płatniczej (dowolne 4 cyfry)");
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
                if (reservation.IsPaymentNecessary)
                    reservation.IsReservationSuccessful = false;
                consoleService.WriteToConsole("Wystąpił błąd w trakcje transakcji");
                logger.LogError(ex, $"Error during payment for {reservation.ReservationNumber}");
            }
        }
    }
}
