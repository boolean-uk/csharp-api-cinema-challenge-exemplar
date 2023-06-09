using System.ComponentModel.DataAnnotations;

namespace api_cinema_challenge.Models.Customer
{
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
