using Microsoft.EntityFrameworkCore;
using hotel1.Model;

namespace hotel1.Model
{

    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers {get;set;}
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }






    }

}