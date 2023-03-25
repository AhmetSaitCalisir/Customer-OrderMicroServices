namespace OrderService.Entities
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
