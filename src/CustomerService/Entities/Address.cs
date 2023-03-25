using System.Text.Json.Serialization;

namespace CustomerService.Entities
{
    public class Address
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int CityCode { get; set; }

        [JsonIgnore]
        public ICollection<Customer>? Customer { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Order { get; set; }
    }
}
