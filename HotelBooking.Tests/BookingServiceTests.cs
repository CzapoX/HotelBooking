﻿using HotelBooking.Data;
using HotelBooking.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace HotelBooking.Tests
{
    public class BookingServiceTests
    {
        private readonly BookingService sut;
        private readonly DateTime reservationDate = DateTime.Now;
        int numberWrittenByUser = 1;
        private readonly List<Hotel> hotels;

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

            sut = new BookingService(consoleService.Object);
        }

        [Fact]
        public void ShouldCreateReservation()
        {
            var reservation = new Reservation();

            sut.BeginBooking(hotels, reservation);


            Assert.Equal(reservation.Date, reservationDate);
            Assert.Equal(reservation.HowManyDays, numberWrittenByUser);
            Assert.True(reservation.IsBookingSuccessful);
            Assert.Equal(reservation.NumberOfPeople, numberWrittenByUser);
            Assert.NotEqual(reservation.ReservationNumber, Guid.Empty);
        }

        [Fact]
        public void NumberShouldBeEqualToUserInput()
        {
            var result = sut.GetNumberOfDaysFromUser();

            Assert.Equal(result, numberWrittenByUser);
        }

        [Fact]
        public void DateShouldBeEqualToReservationDate()
        {
            var result = sut.GetChosenReservationDateFromUser();

            Assert.Equal(result, reservationDate);
        }

        [Fact]
        public void ShouldReturnCorrectHotel()
        {
            var hotel = sut.GetChosenHotelFromUser(hotels);

            var expectedHotel = hotels.Find(x => x.Id == numberWrittenByUser);

            Assert.Equal(hotel, expectedHotel);
        }
    }
}
