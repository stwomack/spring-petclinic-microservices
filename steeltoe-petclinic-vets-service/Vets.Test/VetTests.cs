using Petclinic.Vets.Domain;
using Xunit;

namespace Petclinic.Vets.unit_test
{
    [Collection("Vets Test Collection")]
    public class VetTests
    {
        public VetTests() { }

        [Fact(DisplayName = "Create vet")]
        public void NewOwner()
        {
            var vet = new Vet("AFirst", "ALast");
            Assert.NotNull(vet);
        }
    }
}
