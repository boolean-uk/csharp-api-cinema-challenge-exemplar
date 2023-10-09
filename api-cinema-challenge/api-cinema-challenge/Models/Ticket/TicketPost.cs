using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models.Ticket
{
    [NotMapped]
    public class TicketPost
    {
        public int numSeats { get; set; } = 0;
    }
}
