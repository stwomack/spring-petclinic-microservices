using System;
using System.Collections.Generic;

namespace Petclinic.Vets.Domain
{
    public class Vet
    {
        public Vet(string firstName, string lastName, int id = default)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public int Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public ICollection<VetSpecialty> VetSpecialties{ get; set; }
    }
}
