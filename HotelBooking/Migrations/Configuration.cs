using HotelBooking.Data;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace HotelBooking.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HotelDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HotelDbContext context)
        {
            if (!context.Hotels.Any())
            {
                var hotels = new List<Hotel>
                {
                    new Hotel
                    {
                        Name = "Hotel Stokrotka",
                        Location = "Zakopane",
                        PriceForOnePerson = 150,
                        IsPaymentNecessary = true
                    },

                    new Hotel
                    {
                        Name = "Hotel Biedronka",
                        Location = "Warszawa",
                        PriceForOnePerson = 130,
                        IsPaymentNecessary = false
                    },

                    new Hotel
                    {
                        Name = "Hotel Czekolada",
                        Location = "Wrocław",
                        PriceForOnePerson = 130,
                        IsPaymentNecessary = true
                    },
                   
                    new Hotel
                    {
                        Name = "Hotel Masło",
                        Location = "Poznań",
                        PriceForOnePerson = 130,
                        IsPaymentNecessary = false
                    },
                   
                    new Hotel
                    {
                        Name = "Hotel Gdynia",
                        Location = "Gdynia",
                        PriceForOnePerson = 120,
                        IsPaymentNecessary = false
                    },
                   
                    new Hotel
                    {
                        Name = "Hotel Pod Drzewem",
                        Location = "Zakopane",
                        PriceForOnePerson = 200,
                        IsPaymentNecessary = true
                    },
                };

                context.Hotels.AddRange(hotels);
                context.SaveChanges();
            }
        }
    }
}
