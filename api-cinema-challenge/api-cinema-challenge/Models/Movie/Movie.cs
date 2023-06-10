namespace api_cinema_challenge.Models.Movie
{
    public class Movie
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string rating { get; set; }
        public string description { get; set; }
        public int runtimeMins { get; set; }
    }
}
