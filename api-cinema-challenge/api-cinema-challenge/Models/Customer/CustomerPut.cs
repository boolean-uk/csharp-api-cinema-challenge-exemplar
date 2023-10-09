using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models.Customer
{
    [NotMapped]
    public class CustomerPut
    {
        [Required]
        string name { get; set; }
        [Required]
        string email { get; set; }
        [Required]
        string phone { get; set; }
    }
}
