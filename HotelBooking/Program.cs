using HotelBooking.Data;
using HotelBooking.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                services.AddTransient<IBookingService, BookingService>();
                services.AddTransient<HotelDbContext>();
                services.AddTransient<IConsoleService, ConsoleService>();
                services.AddTransient<IPaymentService, PaymentService>();
                services.AddTransient<IPriceCheckService, PriceCheckService>();
            }).Build();

            var dbContext = ActivatorUtilities.CreateInstance<HotelDbContext>(host.Services);

            var hotelsCached = new List<Hotel>();

            hotelsCached = dbContext.Hotels.ToList();

            var reservation = new Reservation();

            var bookingService = ActivatorUtilities.CreateInstance<BookingService>(host.Services);
            bookingService.BeginBooking(hotelsCached, reservation);

            var priceCheckService = ActivatorUtilities.CreateInstance<PriceCheckService>(host.Services);
            priceCheckService.CheckIfPrizeIsStillCorrect(reservation);

            var paymentService = ActivatorUtilities.CreateInstance<PaymentService>(host.Services);
            paymentService.BeginPayment(reservation);

            if (!CheckIfReservationCanBeContinued(reservation))
            {
                BookingSummary(reservation);
                return;
            }

            var emailSenderService = ActivatorUtilities.CreateInstance<EmailSenderService>(host.Services);
            emailSenderService.SendConfirmationEmail(reservation);

            dbContext.Reservations.Add(reservation);
            dbContext.SaveChanges();

            BookingSummary(reservation);
        }

        private static void BookingSummary(Reservation reservation)
        {
            Console.WriteLine();
            Console.WriteLine($"Podsumowanie rezerwacji o numerze: {reservation.ReservationNumber}");
            Console.WriteLine($"Status rezerwacji: { reservation.IsReservationSuccessful.TranslateToPolish()}");
            Console.WriteLine($"Status procesu wprowadzania informacji: {reservation.IsBookingSuccessful.TranslateToPolish()}");
            Console.WriteLine($"Status płatności: {reservation.IsPaymentSuccessful.TranslateToPolish()}");
            Console.WriteLine($"Status wysłanego maila: {reservation.IsEmailSendSuccessful.TranslateToPolish()}");
            Console.ReadLine();
        }

        private static bool CheckIfReservationCanBeContinued(Reservation reservation)
        {
            if (reservation.IsReservationSuccessful == false)
            {
                Console.WriteLine("Rezerwacja nie powiodła się prosimy spróbować ponownie");
                return false;
            }

            return true;
        }
    }
}