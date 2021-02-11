using HotelBooking.Data;
using Microsoft.Extensions.Logging;
using System;

namespace HotelBooking.Services
{
    interface IEmailSenderService
    {
        void SendConfirmationEmail(Reservation reservation);
    }

    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConsoleService consoleService;
        private readonly ILogger<EmailSenderService> logger;

        public EmailSenderService(IConsoleService consoleService, ILogger<EmailSenderService> logger)
        {
            this.consoleService = consoleService;
            this.logger = logger;
        }

        public void SendConfirmationEmail(Reservation reservation)
        {
            consoleService.WriteToConsole("Potrzebujemy twojego maila, aby wysłać potwierdzenie rezerwacji");
            reservation.Email = consoleService.GetEmailFromUser();

            try
            {
                //TODO 3rd party email sender
                reservation.IsEmailSendSuccessful = true;
                consoleService.WriteToConsole
                    ("Na twojego maila zostało wysłane potwierdzenie");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error when sending email for {reservation.ReservationNumber}");
                reservation.IsEmailSendSuccessful = false;
                consoleService.WriteToConsole
                    ($"Wystąpił błąd w trakcie wysyłania maila dotyczącego rezerwacji {reservation.ReservationNumber}");
            }
        }
    }
}
