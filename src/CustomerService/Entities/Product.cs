﻿using System.Text.Json.Serialization;

namespace CustomerService.Entities
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Order>? Order { get; set; }
    }
}
