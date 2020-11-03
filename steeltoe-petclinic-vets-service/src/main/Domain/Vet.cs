using System;
using System.Collections.Generic;
using System.Linq;

namespace steeltoe_petclinic_vets_api.Domain {
   public class Vet {
    public Vet(string firstName, string lastName, int id = default){
      Id = id;
      FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
      LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
    }

    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
  }
}
