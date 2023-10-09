using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models.Customer
{
    [NotMapped]
    public class CustomerPost
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phone { get; set; }
    }
}
