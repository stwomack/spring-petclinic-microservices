using System.Collections.Generic;
using System.Linq;

namespace Petclinic.Vets.DTOs
{
    public class VetDetails
    {
        public VetDetails() { }

        public VetDetails(int id, string firstName, string lastName, List<SpecialtyDetails> specialties)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Specialties = specialties;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<SpecialtyDetails> Specialties { get; set; }

        public int NrOfSpecialties => Specialties?.Count() ?? 0;
    }
}
