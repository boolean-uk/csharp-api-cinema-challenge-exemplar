using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models.Ticket
{
    [NotMapped]
    public class TicketBooking
    {
        public string status { get; set; } = "success";
        public TicketGet data { get; set; }
    }
}
