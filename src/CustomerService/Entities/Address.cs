﻿namespace CustomerService.Entities
{
    public class Address
    {
        public string Id { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CityCode { get; set; }
        public ICollection<Customer> Customer { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}