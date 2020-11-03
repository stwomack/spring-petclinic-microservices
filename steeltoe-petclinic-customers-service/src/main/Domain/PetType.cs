using System;
using System.Collections.Generic;

namespace steeltoe_petclinic_customers_api.Domain
{
  public partial class PetType {

    public PetType(string name, int id = default) {
      Id = id;
      Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    protected PetType(){
    }

    public int Id { get; set; }
    public string Name { get; set; }
  }
}
