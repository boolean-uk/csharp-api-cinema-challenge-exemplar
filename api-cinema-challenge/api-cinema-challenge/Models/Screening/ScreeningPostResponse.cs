namespace api_cinema_challenge.Models.Screening
{
    public class ScreeningPostResponse
    {
        public string status { get; set; } = "success";
        public Screening data { get; set; }
    }
}
