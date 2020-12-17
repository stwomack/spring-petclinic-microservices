using System;
using System.Collections.Generic;

namespace Petclinic.Vets.Domain
{
    public class Specialty : IComparable
    {
        public Specialty(string name, int id = default)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public ICollection<VetSpecialty> VetSpecialties { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (obj is Specialty other)
            {
                return Id.CompareTo(other.Id);
            }
            else
            {
                throw new ArgumentException("Object is not a specialty");
            }
        }
    }
}
