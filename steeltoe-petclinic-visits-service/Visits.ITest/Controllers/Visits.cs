using Petclinic.Visits.DTOs;
using Petclinic.Visits.Infrastructure;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Petclinic.Visits.ITest.Controllers
{
    [Collection("Vets API Test Collection")]
    public class Visits : IClassFixture<CustomersAppFactory<Startup>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly CustomersAppFactory<Startup> _factory;

        public Visits(CustomersAppFactory<Startup> factory, ITestOutputHelper outputHelper)
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

        [Fact(DisplayName = "GET visit by petId")]
        public async Task GetVisits()
        {
            var petId = Fill.Visits.First().PetId;

            var visits = await _client.GetFromJsonAsync<VisitDetails[]>($"owners/pets/{petId}/visits");

            Assert.NotNull(visits);
            Assert.Equal(Fill.Visits.Where(q => q.PetId == petId).Count(), visits.Count());
        }

        [Fact(DisplayName = "GET visit by petIds")]
        public async Task GetVisitsMultipleIds()
        {
            var petId1 = Fill.Visits.First().PetId;
            var petId2 = Fill.Visits.First(q => q.PetId != petId1).PetId;

            var visits = await _client.GetFromJsonAsync<VisitDetails[]>($"pets/visits?petId={petId1}%2C{petId2}");

            Assert.NotNull(visits);
            Assert.NotEmpty(visits);
        }

        [Fact(DisplayName = "POST new visit")]
        public async Task Save()
        {
            var petId = Fill.Visits.First().PetId;

            var visitRequest = new VisitRequest()
            {
                Description = "Another one",
                VisitDate = DateTime.Now
            };

            var resp = await _client.PostAsJsonAsync($"owners/pets/{petId}/visits", visitRequest);

            Assert.True(resp.IsSuccessStatusCode);

            var vist = await resp.Content.ReadFromJsonAsync<VisitDetails>();

            Assert.NotNull(vist);
            Assert.Equal(visitRequest.Description, vist.description);
        }
    }
}
