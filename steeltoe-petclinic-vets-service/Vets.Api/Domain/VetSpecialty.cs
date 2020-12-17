using Petclinic.Vets.Infrastructure;
using System.Linq;

namespace Petclinic.Vets.Domain
{
    public class VetSpecialty
    {
        public VetSpecialty(int vetId, int specialtyId)
        {
            VetId = vetId;
            SpecialtyId = specialtyId;
            //Specialty = Fill.Specialties.FirstOrDefault(i => i.Id == specialtyId);
            //Vet = Fill.Vets.FirstOrDefault(i => i.Id == vetId);
        }

        public int VetId { get; private set; }

        public Vet Vet { get; set; }

        public int SpecialtyId { get; private set; }

        public Specialty Specialty { get; set; }
    }
}
