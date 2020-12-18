using System;
using System.Text.Json.Serialization;

namespace Petclinic.Customers.DTOs
{
    public class PetRequest
    {
        public PetRequest() { }

        public PetRequest(int id, DateTime? birthDate, string name, string petTypeId)
        {
            Id = id;
            BirthDate = birthDate;
            Name = name;
            TypeId = petTypeId;
        }
        public int Id { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? BirthDate { get; set; }

        public string Name { get; set; }

        public string TypeId { get; set; }
    }
}
