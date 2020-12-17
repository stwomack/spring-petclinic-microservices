using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Petclinic.Visits.Domain;
using Petclinic.Visits.DTOs;
using Petclinic.Visits.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Petclinic.Visits.Controllers
{
    [Route("")]
    [ApiController]
    [Produces("application/json")]
    public class VisitsController : ControllerBase
    {
        private readonly ILogger<VisitsController> _logger;
        private readonly IVisits _visitsRepo;

        public VisitsController(ILogger<VisitsController> logger, IVisits visitsRepo)
        {
            _logger = logger;
            _visitsRepo = visitsRepo;
        }

        [HttpGet("owners/{a}/pets/{petId:int}/visits")]
        [HttpGet("owners/pets/{petId:int}/visits")]
        [ProducesResponseType(typeof(List<VisitDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<VisitDetails>>> Visits(int petId, CancellationToken cancellationToken)
        {
            var visits = await _visitsRepo.FindByPetIdAsync(petId, cancellationToken);

            var ret = new List<VisitDetails>();
            foreach (var visit in visits)
                ret.Add(VisitDetails.FromVisit(visit));

            return Ok(ret);
        }

        [HttpGet("pets/visits")]
        [ProducesResponseType(typeof(List<VisitDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<VisitDetails>>> VisitsMultiGet([FromQuery] string petId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(petId))
                return BadRequest();

            string decodedPetId = HttpUtility.UrlDecode(petId);
            _logger.LogInformation("Decoded string as {@DecodedPetId}", decodedPetId);
            List<int> petIds = petId.Split(',').Select(int.Parse).ToList();
            _logger.LogInformation("Retrieving information for pets {@PetIds}", petIds);

            var visits = _visitsRepo.FindByPetIdIn(petIds);

            _logger.LogInformation("Found {N} visits to return", visits.Count());

            var ret = new List<VisitDetails>();
            foreach (var visit in visits)
                ret.Add(VisitDetails.FromVisit(visit));

            _logger.LogInformation("Formatted {N} visits with detail", ret.Count());

            return Ok(ret);
        }

        [HttpPost("owners/{a}/pets/{petId:int}/visits")]
        [HttpPost("owners/pets/{petId:int}/visits")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> Create(int petId, [FromBody] VisitRequest visitRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Saving visit {visitRequest}");

            var visit = new Visit(petId, visitRequest.VisitDate, visitRequest.Description);
            var newVisit = await _visitsRepo.SaveAsync(petId, visit, cancellationToken);
            return Created($"owners/pets/{petId}/visits", VisitDetails.FromVisit(newVisit));
        }
    }
}
