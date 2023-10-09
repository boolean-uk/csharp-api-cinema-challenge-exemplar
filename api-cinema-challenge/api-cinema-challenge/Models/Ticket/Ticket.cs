using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models.Ticket
{
    public class Ticket
    {
        public int id { get; set; }
        public int numSeats { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Screening")]
        public int ScreeningId { get; set; }
    }
}
