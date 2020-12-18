using Petclinic.Visits.Domain;
using Petclinic.Visits.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace Petclinic.Visits.Test
{
    [Collection("Visits Test Collection")]
    public class VisitsTest
    {
        public VisitsTest() { }

        [Fact(DisplayName = "Create visit")]
        public void NewOwner()
        {
            var visit = new Visit(1, DateTime.Now, "a description");
            Assert.NotNull(visit);
        }

        [Fact(DisplayName = "Set petId")]
        public void SetPetIdsTest()
        {
            var visit = Fill.Visits.First();
            visit.SetPetId(2);
            Assert.Equal(2, visit.PetId);
        }
    }
}
