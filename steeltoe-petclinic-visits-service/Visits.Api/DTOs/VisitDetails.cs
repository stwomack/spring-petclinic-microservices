using Petclinic.Visits.Domain;
using System;
using System.Text.Json.Serialization;

namespace Petclinic.Visits.DTOs
{
    public class VisitDetails
    {
        public VisitDetails() { }

        public VisitDetails(int id, int petId, DateTime? date, string description)
        {
            Id = id;
            this.petId = petId;
            Date = date;
            this.description = description;
        }

        public int Id { get; set; }

        public int petId { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Date { get; set; }

        public string description { get; set; }

        public static VisitDetails FromVisit(Visit visit)
        {
            return new VisitDetails()
            {
                Id = visit.Id,
                petId = visit.PetId,
                Date = visit.Date,
                description = visit.Description
            };
        }
    }
}
