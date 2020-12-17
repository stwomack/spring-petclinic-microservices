using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Petclinic.Customers.DTOs;
using Petclinic.Customers.Repository;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Petclinic.Customers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OwnersController : ControllerBase
    {
        private readonly ILogger<OwnersController> _logger;
        private readonly IOwners _ownersRepo;

        public OwnersController(ILogger<OwnersController> logger, IOwners ownersRepo)
        {
            _logger = logger;
            _ownersRepo = ownersRepo;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateOwner([FromBody] OwnerRequest ownerRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Saving owner {ownerRequest}");
            var owner = await _ownersRepo.Save(ownerRequest.ToOwner(), cancellationToken);
            return Created($"owners/{owner.Id}", OwnerDetails.FromOwner(owner));
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(typeof(OwnerDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OwnerDetails>> FindOwner(int ownerId, CancellationToken cancellationToken)
        {
            var owner = await _ownersRepo.FindById(ownerId, cancellationToken);
            return Ok(OwnerDetails.FromOwner(owner));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OwnerDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<OwnerDetails>>> FindAll(CancellationToken cancellationToken)
        {
            var owners = await _ownersRepo.FindAll(cancellationToken);

            var ret = new List<OwnerDetails>();
            foreach (var owner in owners)
            {
                ret.Add(OwnerDetails.FromOwner(owner));
            }

            return Ok(ret);
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> ProcessUpdateForm(int ownerId, [FromBody] OwnerRequest ownerRequest, CancellationToken cancellationToken)
        {
            var owner = await _ownersRepo.FindById(ownerId, cancellationToken);

            if (owner == null)
            {
                throw new ResourceNotFoundException("Owner " + ownerId + " not found");
            }

            _logger.LogInformation($"Updating owner {ownerRequest}");
            await _ownersRepo.Update(owner, ownerRequest.ToOwner(), cancellationToken);

            return NoContent();
        }
    }
}
