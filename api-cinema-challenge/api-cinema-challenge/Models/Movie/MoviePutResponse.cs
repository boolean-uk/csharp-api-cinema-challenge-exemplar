namespace api_cinema_challenge.Models.Movie
{
    public class MoviePutResponse
    {
        public string status { get; set; } = "success";
        public Movie data { get; set; }
    }
}
