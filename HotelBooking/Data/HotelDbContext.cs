using System.Data.Entity;

namespace HotelBooking.Data
{
    class HotelDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
    }
}
