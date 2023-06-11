using api_cinema_challenge.Models.Customer;
using api_cinema_challenge.Models.Movie;
using api_cinema_challenge.Models.Screening;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace api_cinema_challenge.Data
{
    public class DatabaseContext : DbContext
    {
        private static string GetConnectionString()
        {
            string jsonSettings = File.ReadAllText("appsettings.json");
            JObject configuration = JObject.Parse(jsonSettings);

            return configuration["ConnectionStrings"]["DefaultConnectionString"].ToString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseInMemoryDatabase(databaseName: "Library");            
            optionsBuilder.UseNpgsql(GetConnectionString());


        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
    }
}
