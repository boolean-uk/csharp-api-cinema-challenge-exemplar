namespace api_cinema_challenge.Models.Customer
{
    public interface ICustomer
    {
        int Id { get; set; }
        string name { get; set; }
        string email { get; set; }
        string phone { get; set; }
        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
    }
}
