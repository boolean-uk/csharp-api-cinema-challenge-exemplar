using api_cinema_challenge.Models.Screening;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models.Movie
{
    [NotMapped]
    public class MoviePost
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string rating { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public int runtimeMins { get; set; }

        public IEnumerable<ScreeningPost> screenings { get; set; }
    }
}
