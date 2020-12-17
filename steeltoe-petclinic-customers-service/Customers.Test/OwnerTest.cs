using Petclinic.Customers.Domain;
using Petclinic.Customers.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace Petclinic.Customers.Test
{
    [Collection("Pets Test Collection")]
    public class OwnerTest
    {
        public OwnerTest()
        {
        }

        [Fact(DisplayName = "Create owner")]
        public void NewOwner()
        {
            var owner = new Owner("first", "last", "123 street rd", "Some City", "123-123-1234");
            Assert.NotNull(owner);
        }

        [Fact(DisplayName = "Add Pet")]
        public void AddPetTest()
        {
            var owner = Fill.Owners.First();
            var petTypeId = Fill.PetTypes.First().Id;
            var pet = new Pet("Herbert", DateTime.Now, petTypeId, owner.Id);
            var expectedResult = owner.Pets.Count() + 1;

            owner.AddPet(pet);

            Assert.Equal(expectedResult, owner.Pets.Count());
        }

        [Fact(DisplayName = "To string")]
        public void ToStringTest()
        {
            var owner = Fill.Owners.First();

            var str = owner.ToString();

            Assert.NotNull(str);
        }

        [Fact(DisplayName = "Set first name")]
        public void SetFirstNameTest()
        {
            var owner = Fill.Owners.First();
            owner.SetFirstName("asdf");
            Assert.Equal("asdf", owner.FirstName);
        }

        [Fact(DisplayName = "Set city")]
        public void SetCityTest()
        {
            var owner = Fill.Owners.First();
            owner.SetCity("asdf");
            Assert.Equal("asdf", owner.City);
        }

        [Fact(DisplayName = "Set last name")]
        public void SetLastNameTest()
        {
            var owner = Fill.Owners.First();
            owner.SetLastName("asdf");
            Assert.Equal("asdf", owner.LastName);
        }

        [Fact(DisplayName = "Set telephone")]
        public void SetTelephoneTest()
        {
            var owner = Fill.Owners.First();
            owner.SetTelephone("asdf");
            Assert.Equal("asdf", owner.Telephone);
        }

        [Fact(DisplayName = "Set address")]
        public void SetAddressTest()
        {
            var owner = Fill.Owners.First();
            owner.SetAddress("asdf");
            Assert.Equal("asdf", owner.Address);
        }
    }
}
