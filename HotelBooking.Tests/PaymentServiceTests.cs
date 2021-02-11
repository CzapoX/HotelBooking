using HotelBooking.Data;
using HotelBooking.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HotelBooking.Tests
{
    public class PaymentServiceTests
    {
        PaymentService sut;

        public PaymentServiceTests()
        {
            Mock<ILogger<PaymentService>> logger = new Mock<ILogger<PaymentService>>();
            Mock<IConsoleService> consoleService = new Mock<IConsoleService>();
            consoleService.Setup(e => e.GetCreditCardFromUser()).Returns(1111);
            consoleService.Setup(e => e.GetBoolFromUser()).Returns(false);

            sut = new PaymentService(consoleService.Object, logger.Object);
        }

        [Fact]
        public void PaymentShouldFail()
        {
            var reservation = new Reservation
            {
                IsPaymentNecessary = false
            };

            sut.BeginPayment(reservation);

            Assert.False(reservation.IsPaymentSuccessful);
            Assert.False(reservation.IsReservationSuccessful);
        }

        [Fact]
        public void PaymentShouldBeSuccess()
        {
            var reservation = new Reservation
            {
                IsPaymentNecessary = true
            };

            sut.BeginPayment(reservation);

            Assert.True(reservation.IsPaymentSuccessful);
        }
    }
}