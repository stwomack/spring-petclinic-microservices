using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Petclinic.Customers.Domain;
using Petclinic.Customers.DTOs;
using Petclinic.Customers.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Petclinic.Customers.Controllers
{
    [Route("")]
    [ApiController]
    [Produces("application/json")]
    public class PetsController : ControllerBase
    {
        private readonly IPets _petsRepo;
        private readonly ILogger<PetsController> _logger;
        private readonly IOwners _ownersRepo;

        public PetsController(ILogger<PetsController> logger, IPets petsRepo, IOwners ownersRepo)
        {
            _petsRepo = petsRepo;
            _logger = logger;
            _ownersRepo = ownersRepo;
        }

        [HttpGet("petTypes")]
        [ProducesResponseType(typeof(List<DTOs.PetType>), 200)]
        public async Task<ActionResult<List<DTOs.PetType>>> GetPetTypes(CancellationToken cancellationToken)
        {
            var petTypes = await _petsRepo.FindPetTypes(cancellationToken);

            var ret = petTypes.Select(petType => DTOs.PetType.ToDTO(petType));
            return Ok(ret);
        }

        [HttpGet("owners/{ownerId}/pets/{petId:int}")]
        [ProducesResponseType(typeof(PetDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PetDetails>> FindPet(int petId, CancellationToken cancellationToken)
        {
            var pet = await _petsRepo.FindById(petId, cancellationToken);

            if (pet == null)
            {
                throw new ResourceNotFoundException("Pet " + petId + " not found");
            }

            return Ok(new PetDetails(pet.Id, pet.Name, pet.Owner.FirstName + " " + pet.Owner.LastName, pet.BirthDate, DTOs.PetType.ToDTO(pet.PetType)));
        }

        [HttpPost("owners/{ownerId}/pets")]
        [ProducesResponseType(typeof(PetDetails), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ResourceNotFoundException), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PetDetails>> ProcessCreationForm(int ownerId, [FromBody] PetRequest petRequest, CancellationToken cancellationToken)
        {
            var owner = await _ownersRepo.FindById(ownerId, cancellationToken);
            _logger.LogInformation($"Saving pet {petRequest}");

            var newPet = new Pet(petRequest.Name, petRequest.BirthDate, int.Parse(petRequest.TypeId), ownerId)
            {
                PetType = await _petsRepo.FindPetTypeById(int.Parse(petRequest.TypeId), cancellationToken),
                Owner = owner ?? throw new ResourceNotFoundException("Owner " + ownerId + " not found")
            };

            await _petsRepo.Save(newPet, cancellationToken);

            return Created($"owners/pets/{newPet.Id}", new PetDetails(newPet.Id, newPet.Name, newPet.Owner.FirstName + " " + newPet.Owner.LastName, newPet.BirthDate, DTOs.PetType.ToDTO(newPet.PetType)));
        }

        [HttpPut("owners/{ownerId}/pets/{petId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> ProcessUpdateForm(int petId, [FromBody] PetRequest petRequest, CancellationToken cancellationToken)
        {
            var pet = await _petsRepo.FindById(petId, cancellationToken);

            if (pet == null)
            {
                throw new ResourceNotFoundException("Pet " + petId + " not found");
            }

            _logger.LogInformation($"Updating pet {petRequest}");

            pet.SetBirthDate(petRequest.BirthDate);
            pet.SetName(petRequest.Name);

            await _petsRepo.Update(pet, cancellationToken);

            return NoContent();
        }
    }
}
