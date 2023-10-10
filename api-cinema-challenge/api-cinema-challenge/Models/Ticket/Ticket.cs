using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_cinema_challenge.Models.Ticket
{
    public class Ticket
    {
        public int id { get; set; }
        public int numSeats { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        
        [JsonIgnore]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        
        [JsonIgnore]
        [ForeignKey("Screening")]
        public int ScreeningId { get; set; }
    }
}
