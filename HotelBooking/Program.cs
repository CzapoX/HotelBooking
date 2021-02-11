using HotelBooking.Data;
using HotelBooking.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            }).Build();

            var dbContext = ActivatorUtilities.CreateInstance<HotelDbContext>(host.Services);

            var hotelsCached = new List<Hotel>();

            hotelsCached = dbContext.Hotels.ToList();

            var reservation = new Reservation();

            var bookingService = ActivatorUtilities.CreateInstance<BookingService>(host.Services);
            bookingService.BeginBooking(hotelsCached, reservation);

            var paymentService = ActivatorUtilities.CreateInstance<PaymentService>(host.Services);
            paymentService.BeginPayment(reservation);
        }
    }
}