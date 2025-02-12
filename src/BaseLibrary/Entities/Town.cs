﻿using System.Text.Json.Serialization;

namespace BaseLibrary.Entities
{
    public class Town : BaseEntity
    {
        // Relationships : one-to-many
        //[JsonIgnore]
        //public List<User>? Users { get; set; }
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }

        // Relationships : Many to One
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
