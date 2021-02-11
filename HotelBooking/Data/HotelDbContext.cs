using System.Data.Entity;

namespace HotelBooking.Data
{
    class HotelDbContext : DbContext
    {
        public HotelDbContext() : base("ConnectionString")
        {

        }


        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
