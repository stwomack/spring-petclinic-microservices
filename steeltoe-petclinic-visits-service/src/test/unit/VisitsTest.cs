using System;
using Xunit;
using System.Linq;
using steeltoe_petclinic_visits_api.Infrastructure;
using steeltoe_petclinic_visits_api.Domain;

namespace steeltoe_petclinic_visits_unit_test {
  [Collection("Visits Test Collection")]
  public class VisitsTest {
    public VisitsTest() { }
    [Fact(DisplayName = "Create visit")]
    public void NewOwner() {
      var visit = new Visit(1, DateTime.Now,"a description");
      Assert.NotNull(visit);
    }
    [Fact(DisplayName = "Set petId")]
    public void SetPetIdsTest() {
      var visit = Fill.Visits.First();
      visit.SetPetId(2);
      Assert.Equal(2, visit.PetId);
    }
  }
}
