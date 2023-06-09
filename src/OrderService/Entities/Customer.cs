﻿using System.Text.Json.Serialization;

namespace OrderService.Entities
{
    public class Customer : EntityBase
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual Address? Address { get; set; }

        public string AddressId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Order>? Orders { get; set; }
    }
}
