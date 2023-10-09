namespace api_cinema_challenge.Models.Screening
{
    public class ScreeningGetResponse
    {
        public string status { get; set; } = "success";
        public IEnumerable<Screening> data { get; set; }
    }
}
