using Petclinic.Customers.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Petclinic.Customers.DTOs
{
    public class OwnerDetails
    {
        public OwnerDetails() { }

        public OwnerDetails(int id, string firstName, string lastName, string address, string city, string telephone, List<PetDetails> pets)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            Telephone = telephone;
            Pets = pets;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Telephone { get; set; }

        public List<PetDetails> Pets { get; set; }

        public static OwnerDetails FromOwner(Owner owner)
        {
            return new OwnerDetails()
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Address = owner.Address,
                City = owner.City,
                Telephone = owner.Telephone,
                Pets = owner.Pets.Select(q => new PetDetails(q.Id, q.Name, owner.FirstName + " " + owner.LastName, q.BirthDate, PetType.ToDTO(q.PetType))).ToList()
            };
        }
    }
}
