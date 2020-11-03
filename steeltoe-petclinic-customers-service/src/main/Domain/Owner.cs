using System;
using System.Collections.Generic;

namespace steeltoe_petclinic_customers_api.Domain {

  public class Owner {
    private readonly List<Pet> _pets;

    public Owner(string firstName, string lastName, string address, string city, string telephone, int id = default){
      FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
      LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
      Address = address ?? throw new ArgumentNullException(nameof(address));
      City = city  ?? throw new ArgumentNullException(nameof(city));
      Telephone = telephone ?? throw new ArgumentNullException(nameof(telephone));

      Id = id;
      _pets = new List<Pet>();
    }

    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string Telephone { get; private set; }

    public virtual IReadOnlyCollection<Pet> Pets => _pets?.AsReadOnly() ?? new List<Pet>().AsReadOnly();
    //{
    //  get { return Pets.OrderByDescending(q => q.Name).ToList(); }
    //  set { Pets = value;  }
    //}

    public void AddPet(Pet pet) {
      pet.Owner = this;
      _pets.Add(pet);
    }

    public override string ToString()
    {
      return $@"id:{Id},
  lastName:{LastName},
  firstName:{FirstName},
  address:{Address},
  city:{City},
  telephone:{Telephone}";
    }

    public void SetFirstName(string firstName) {
      FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
    }

    public void SetCity(string city) {
      City = city ?? throw new ArgumentNullException(nameof(city));
    }

    public void SetLastName(string lastName) {
      LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
    }

    public void SetTelephone(string telephone) {
      Telephone = telephone ?? throw new ArgumentNullException(nameof(telephone));
    }

    public void SetAddress(string address) {
      Address = address ?? throw new ArgumentNullException(nameof(address));
    }

  }
}
