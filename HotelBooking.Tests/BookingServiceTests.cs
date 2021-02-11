using HotelBooking.Data;
using HotelBooking.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HotelBooking.Tests
{
    public class BookingServiceTests
    {
        private readonly BookingService sut;
        private readonly DateTime reservationDate = DateTime.Now;
        int numberWrittenByUser = 1;
        private readonly List<Hotel> hotels;
        private readonly Hotel expectedHotel;

        public BookingServiceTests()
        {
            Mock<IConsoleService> consoleService = new Mock<IConsoleService>();
            consoleService.Setup(e => e.GetDateTimeFromUser()).Returns(reservationDate);
            consoleService.Setup(e => e.GetNumberFromUser()).Returns(numberWrittenByUser);
            hotels = new List<Hotel>
            {
                new Hotel
                {
                   Id = 1,
                   Name = "Hotel Niebieski"
                }
             };

            expectedHotel = hotels.FirstOrDefault(x => x.Id == numberWrittenByUser);


            sut = new BookingService(consoleService.Object);
        }

        [Fact]
        public void ShouldCreateReservation()
        {
            var reservation = new Reservation();

            sut.BeginBooking(hotels, reservation);


            Assert.Equal(reservation.Date, reservationDate);
            Assert.Equal(reservation.Hotel, expectedHotel);
            Assert.Equal(reservation.HowManyDays, numberWrittenByUser);
            Assert.True(reservation.IsBookingSuccessful);
            Assert.Equal(reservation.NumberOfPeople, numberWrittenByUser);
            Assert.NotEqual(reservation.ReservationNumber, Guid.Empty);
        }
    }
}
