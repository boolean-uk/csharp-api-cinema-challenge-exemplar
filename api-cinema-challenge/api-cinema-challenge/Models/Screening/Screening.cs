namespace api_cinema_challenge.Models.Screening
{
    public class Screening
    {
        public int Id { get; set; } 
        public int screenNumber { get; set; }
        public int capacity { get; set; }
        public DateTime startsAt { get; set; }

    }
}
