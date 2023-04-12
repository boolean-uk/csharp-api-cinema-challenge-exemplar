using Microsoft.EntityFrameworkCore;

using api_cinema_challenge.CSharp.Main.Models;

namespace api_cinema_challenge.CSharp.Main
{
    public class CinemaDataContext : DbContext
    {
        public CinemaDataContext(DbContextOptions<CinemaDataContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId);

            modelBuilder.Entity<Screening>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Screening)
                .HasForeignKey(e => e.ScreeningId);

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Screenings)
                .WithOne(e => e.Movie)
                .HasForeignKey(e => e.MovieId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}