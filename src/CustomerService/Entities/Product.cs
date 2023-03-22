namespace CustomerService.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
