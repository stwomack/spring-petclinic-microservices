using Petclinic.Vets.Domain;
using System.Collections.Generic;

namespace Petclinic.Vets.Infrastructure.Repository
{
    public interface IVets
    {
        IEnumerable<Vet> FindAll();
    }
}
