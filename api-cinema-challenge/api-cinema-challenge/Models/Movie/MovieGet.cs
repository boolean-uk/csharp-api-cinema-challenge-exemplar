namespace api_cinema_challenge.Models.Movie
{
    public class MovieGet
    {
        public string status { get; set; } = "status";
        public IEnumerable<Movie> data { get; set;}
    }
}
