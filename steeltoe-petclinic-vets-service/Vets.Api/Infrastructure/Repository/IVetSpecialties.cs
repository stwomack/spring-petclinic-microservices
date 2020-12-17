using Petclinic.Vets.Domain;
using System.Collections.Generic;

namespace Petclinic.Vets.Infrastructure.Repository
{
    public interface IVetSpecialties
    {
        IEnumerable<VetSpecialty> FindAllBySpecialtyId(int specialtyId);

        IEnumerable<VetSpecialty> FindAllByVetId(int vetId);
    }
}
