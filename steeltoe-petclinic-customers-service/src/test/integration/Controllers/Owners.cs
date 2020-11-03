using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using steeltoe_petclinic_customers_api;
using System.Linq;
using steeltoe_petclinic_customers_api.Infrastructure;
using steeltoe_petclinic_customers_api.DTOs;

namespace steeltoe_petclinic_customers_integration_test.Controllers
{
  [Collection("Owners API Test Collection")]
  public class Owners : IClassFixture<CustomersAppFactory<Startup>>, IDisposable
  {
    private readonly HttpClient _client;
    private readonly CustomersAppFactory<Startup> _factory;

    public Owners(CustomersAppFactory<Startup> factory, ITestOutputHelper outputHelper)
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
    [Fact(DisplayName = "GET all owners")]
    public async Task FindAll() {
      var owners = await _client.GetFromJsonAsync<OwnerDetails[]>("owners");

      Assert.NotNull(owners);
      Assert.True(owners.Count() >= Fill.Owners.Count());//POST test could get run before this
      foreach (var owner in owners)
        Assert.Equal(Fill.Pets.Where(q => q.OwnerId == owner.Id).Count(), owner.Pets.Count());
    }
    [Fact(DisplayName = "GET owner")]
    public async Task FindOwner() {
      var o = Fill.Owners.First(q => q.Id==3); //don't use the first owner because it could be augmented in other tests

      var owner = await _client.GetFromJsonAsync<OwnerDetails>($"owners/{o.Id}");

      Assert.NotNull(owner);
      Assert.Equal(o.Id, owner.Id);
      Assert.Equal(Fill.Pets.Where(q => q.OwnerId == owner.Id).Count(), owner.Pets.Count());
      Assert.NotNull(owner.Pets.First().Name);
      Assert.NotNull(owner.Pets.First().Type);
    }
    [Fact(DisplayName = "POST new owner")]
    public async Task CreateOwner() {
      var ownerReq = new OwnerRequest("Some", "One", "123 Street Rd", "City", "45645645655");
 
      var resp = await _client.PostAsJsonAsync($"owners", ownerReq);

      Assert.True(resp.IsSuccessStatusCode);

      var owner = await resp.Content.ReadFromJsonAsync<OwnerDetails>();

      Assert.NotNull(owner);
    }
    [Fact(DisplayName = "PUT existing owner")]
    public async Task ProcessUpdateForm() {
      var owner = Fill.Owners.First();
      owner.SetFirstName("aaaaaa");
      var ownerReq = new OwnerRequest(owner.FirstName, owner.LastName, owner.Address, owner.City, owner.Telephone, owner.Id);

      var resp = await _client.PutAsJsonAsync($"owners/{owner.Id}", ownerReq);

      Assert.True(resp.IsSuccessStatusCode);
    }
  }
}
