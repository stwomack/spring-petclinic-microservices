using steeltoe_petclinic_customers_api.Domain;

namespace steeltoe_petclinic_customers_api.DTOs {
  public class OwnerRequest {
    public OwnerRequest() { }

    public OwnerRequest(string firstName, string lastName, string address, string city, string telephone, int id = default) {
      Id = id;
      FirstName = firstName;
      LastName = lastName;
      Address = address;
      City = city;
      Telephone = telephone;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Telephone { get; set; }

    public Owner ToOwner(){
      return new Owner(FirstName, LastName, Address, City, Telephone, Id);
    }
  }
}
