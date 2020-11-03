using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using steeltoe_petclinic_vets_api;
using steeltoe_petclinic_vets_api.DTOs;
using System.Linq;
using steeltoe_petclinic_vets_api.Infrastructure;

namespace steeltoe_petclinic_vets_integration_test.Controllers
{
  [Collection("Vets API Test Collection")]
  public class Vets : IClassFixture<CustomersAppFactory<Startup>>, IDisposable
  {
    private readonly HttpClient _client;
    private readonly CustomersAppFactory<Startup> _factory;

    public Vets(CustomersAppFactory<Startup> factory, ITestOutputHelper outputHelper)
    {
      factory.OutputHelper = outputHelper;
      _factory = factory;
      _client = _factory.CreateClient();
    }
    public void Dispose()
    {
      //_client.Dispose();
      //_factory.OutputHelper = null;
      //_factory.Dispose();
    }

    [Fact(DisplayName = "GET Health")]
    public async Task Health()
    {
      var respObj = await _client.GetFromJsonAsync<object>("actuator/health");
      Assert.NotNull(respObj);
    }

    [Fact(DisplayName = "GET vets")]
    public async Task FindPet() {
      var vets = await _client.GetFromJsonAsync<VetDetails[]>($"vets");

      Assert.NotNull(vets);
      Assert.Equal(Fill.Vets.Count(), vets.Count());
      foreach (var vet in vets)
        Assert.Equal(Fill.VetSpecialties.Where(q => q.VetId == vet.Id).Count(), vet.Specialties.Count());
    }
  }
}
