namespace CustomerService.Models
{
    public class CustomerModel
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Email { get; set; }

        public string AddressId { get; set; }
    }
}
