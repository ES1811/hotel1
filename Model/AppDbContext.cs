using Microsoft.EntityFrameworkCore;
using hotel1.Model;

namespace hotel1.Model
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
            .HasOne(c => c.Customer)
            .WithOne(r => r.Room)
            .HasForeignKey<Customer>(k => k.RoomId);

            modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Room)
            .WithMany()
            .HasForeignKey(r=> r.RoomId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany()
            .HasForeignKey(r=> r.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

            // modelBuilder.Entity<Customer>()
            // .HasIndex(k => k.RoomId)
            // .IsUnique()
            // .HasFilter(null);

            // modelBuilder.Entity<Customer>()
            // .HasOne(r=>r.Room)
            // .WithOne(c=>c.Customer)
            // .HasForeignKey<Room>(k=>k.CustomerId);
        }
    }

}