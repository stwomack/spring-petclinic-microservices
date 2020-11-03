using System;
using Xunit;
using steeltoe_petclinic_vets_api.Domain;

namespace steeltoe_petclinic_vets_unit_test {
  [Collection("Vets Test Collection")]
  public class VetTests {
    public VetTests() { }

    [Fact(DisplayName = "Create vet")]
    public void NewOwner() {
      var vet = new Vet("AFirst","ALast");
      Assert.NotNull(vet);
    }
  }
}
