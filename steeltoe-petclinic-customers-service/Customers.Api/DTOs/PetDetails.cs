using System;
using System.Text.Json.Serialization;

namespace Petclinic.Customers.DTOs
{
    public class PetDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? BirthDate { get; set; }

        public PetType Type { get; set; }

        public PetDetails() { }

        public PetDetails(int id, string name, string owner, DateTime? birthDate, PetType petType)
        {
            Id = id;
            Name = name;
            Owner = owner;
            BirthDate = birthDate;
            Type = petType;
        }
    }
}
