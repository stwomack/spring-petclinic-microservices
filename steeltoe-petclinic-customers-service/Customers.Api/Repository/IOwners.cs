using Petclinic.Customers.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Petclinic.Customers.Repository
{
    public interface IOwners
    {
        Task Delete(Owner Owner, CancellationToken cancellationToken = default);

        Task<List<Owner>> FindAll(CancellationToken cancellationToken = default);

        Task<List<Owner>> FindAll(int page, int pageSize, CancellationToken cancellationToken = default);

        Task<Owner> FindById(int id, CancellationToken cancellationToken = default);

        Task<Owner> Save(Owner owner, CancellationToken cancellationToken = default);

        Task<Owner> Update(Owner owner, Owner newOwnerVals, CancellationToken cancellationToken = default);
    }
}
