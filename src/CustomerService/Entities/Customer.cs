namespace CustomerService.Entities
{
    public class Customer : EntityBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual Address Address { get; set; }
        public string AddressId { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
