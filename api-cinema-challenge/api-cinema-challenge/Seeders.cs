using api_cinema_challenge.CSharp.Main.Models;

namespace api_cinema_challenge.CSharp.Main
{
    public static class DataSeeder
    {
        private static Customer customerData = new Customer
        {
            Name = "Customer",
            Email = "email@email.com",
            Phone = 1234,
        };
        private static Movie movieData = new Movie
        {
            Title = "Movie",
            Rating = "Good!",
            Description = "A movie about movies",
            RuntimeMins = 3600,
        };
        private static Screening screeningData = new Screening
        {
            ScreenNumber = 1,
            StartsAt = DateTime.UtcNow,
            Capacity = 100,
        };
        private static Ticket ticketData = new Models.Ticket
        {
            NumberOfSeats = 10
        };
        public static void seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<CinemaDataContext>();
            context.Database.EnsureCreated();


            addCustomers(context);
            addMovies(context);
            addScreenings(context);
            addTickets(context);
        }

        private static void addCustomers(CinemaDataContext context)
        {
            var customer = context.Customers.FirstOrDefault();
            if (customer != null) return;

            context.Customers.Add(customerData);
            
            context.SaveChanges();
        }
        private static void addMovies(CinemaDataContext context)
        {
            var movie = context.Movies.FirstOrDefault();
            if (movie != null) return;
            context.Movies.Add(movieData);

            context.SaveChanges();
        }
        private static void addScreenings(CinemaDataContext context)
        {
            var screening = context.Screenings.FirstOrDefault();
            if (screening != null) return;
            screeningData.Movie = movieData;
            screeningData.MovieId = movieData.Id;
            context.Screenings.Add(screeningData);

            context.SaveChanges();
        }
        private static void addTickets(CinemaDataContext context)
        {
            var ticket = context.Tickets.FirstOrDefault();
            if (ticket != null) return;
            ticketData.Customer = customerData;
            ticketData.CustomerId = customerData.Id;
            ticketData.Screening = screeningData;
            ticketData.ScreeningId = screeningData.Id;

            context.Tickets.Add(ticketData);

            context.SaveChanges();
        }
    }
}
