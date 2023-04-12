using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace api_cinema_challenge.CSharp.Main.Models
{
    public class Movie
    {
        public Movie() {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Rating { get; set; }
        public string Description { get; set; }
        public int RuntimeMins { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        [JsonIgnore]
        public ICollection<Screening>? Screenings { get; set; }
    }
}
