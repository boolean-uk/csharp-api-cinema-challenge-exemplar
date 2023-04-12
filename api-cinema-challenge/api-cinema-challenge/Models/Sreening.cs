using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace api_cinema_challenge.CSharp.Main.Models
{
    public class Screening
    {
        public Screening()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int ScreenNumber { get; set; }
        public DateTime StartsAt { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
