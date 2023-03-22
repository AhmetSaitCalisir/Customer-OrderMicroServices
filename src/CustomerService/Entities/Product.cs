namespace CustomerService.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public Order Order { get; set; }
    }
}
