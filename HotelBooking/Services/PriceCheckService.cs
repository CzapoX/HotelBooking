using HotelBooking.Data;
using System.Linq;

namespace HotelBooking.Services
{
    interface IPriceCheckService
    {
        void CheckIfPrizeIsStillCorrect(Reservation reservation);
    }

    class PriceCheckService : IPriceCheckService
    {
        HotelDbContext dbContext;
        private readonly IConsoleService consoleService;

        public PriceCheckService(HotelDbContext dbContext, IConsoleService consoleService)
        {
            this.dbContext = dbContext;
            this.consoleService = consoleService;
        }

        public HotelDbContext DbContext { get; }

        public void CheckIfPrizeIsStillCorrect(Reservation reservation)
        {
            var priceFromDb = dbContext.Hotels.FirstOrDefault(x => x.Id == reservation.HotelId).PriceForOnePerson;
            if (priceFromDb != reservation.BasePrice)
            {
                consoleService.WriteToConsole
                    ($"Cena rezerwacji uległa zmianie, nowa cena za noc, na jedną osobę to: {priceFromDb}");
                consoleService.WriteToConsole("Czy chcesz kontynować rezerwację? (Y/N)");

                if (consoleService.GetBoolFromUser() == false)
                {
                    reservation.IsPaymentSuccessful = false;
                    reservation.IsReservationSuccessful = false;
                }
                else
                {
                    reservation.PriceToPay = priceFromDb * reservation.NumberOfPeople * reservation.HowManyDays;
                }
            }
        }
    }
}
