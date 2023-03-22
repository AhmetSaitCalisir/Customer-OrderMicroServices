namespace CustomerService.Entities
{
    public class Order : EntityBase
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string AddressId { get; set; }
        public string ProductId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }
        public virtual Product Product { get; set; }
    }
}
