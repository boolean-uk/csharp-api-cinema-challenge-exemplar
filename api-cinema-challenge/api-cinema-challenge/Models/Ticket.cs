using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace api_cinema_challenge.CSharp.Main.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
        [Key]
        public int Id { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey("ScreeningId")]
        public int ScreeningId { get; set; }
        public Screening? Screening { get; set; }
    }
}
