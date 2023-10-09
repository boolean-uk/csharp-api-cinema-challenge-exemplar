using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models.Movie
{
    [NotMapped]
    public class MoviePostResult
    {
        public string status { get; set; } = "success";
        public Movie data { get; set; }
    }
}
