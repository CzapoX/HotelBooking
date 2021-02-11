using HotelBooking.Data;
using HotelBooking.Services;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Tests
{
    public class EmailSenderServiceTests
    {
        readonly string email = "a@a.a";
        private readonly EmailSenderService sut;

        public EmailSenderServiceTests()
        {
            Mock<ILogger<EmailSenderService>> logger = new Mock<ILogger<EmailSenderService>>();
            Mock<IConsoleService> consoleService = new Mock<IConsoleService>();
            consoleService.Setup(e => e.GetEmailFromUser()).Returns(email);

            sut = new EmailSenderService(consoleService.Object, logger.Object);
        }

        [Fact]
        public void ShouldEmailSendBeSuccess()
        {
            Reservation reservation = new Reservation();

            sut.SendConfirmationEmail(reservation);

            Assert.True(reservation.IsEmailSendSuccessful);
        }
    }
}
