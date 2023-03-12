using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace GolfClubApp.Data {
    public class Context : DbContext
    {
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Tee> Tees { get; set; }
        public DbSet<TeeBooking> TeeBookings { get; set; }
        public string DatabasePathPointer { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}\\GolfClubApp.db")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public Context() 
        {
            var folderPointer = AppDomain.CurrentDomain.BaseDirectory;
            DatabasePathPointer = Path.Join(folderPointer, "GolfClubApp.db");
        }
    }

    public class Golfer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string EmailAddress { get; set; } = null!;
        [Required]
        public string Sex { get; set; } = null!;
        public int Handicap { get; set; }

        public List<TeeBooking> TeeBookings { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

    }

    public class Tee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<TeeBooking> Bookings { get; set; }
    }

    public class TeeBooking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime BookingTime { get; set; }
        public List<Golfer> Golfers { get; set; }

        [Required]
        public Tee BookedTee { get; set; }
    }
}