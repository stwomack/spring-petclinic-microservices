using System;
using System.Collections.Generic;

namespace Petclinic.Customers.Domain
{
    public partial class PetType
    {
        public PetType(string name, int id = default)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        protected PetType()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();
    }
}
