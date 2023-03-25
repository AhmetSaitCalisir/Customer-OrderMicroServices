namespace OrderService.Models
{
    public class OrderModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public string AddressId { get; set; }
        public string ProductId { get; set; }
    }
}
