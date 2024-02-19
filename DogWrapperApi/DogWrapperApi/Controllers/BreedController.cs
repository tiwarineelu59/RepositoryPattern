using DogWrapperApi.Contracts;
using DogWrapperApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DogWrapperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly IBreedRepository _breedRepo;
        private readonly ILogger<BreedController> _logger;

        public BreedController(IBreedRepository breedRepo, ILogger<BreedController> logger)
        {
            _breedRepo = breedRepo;
            _logger = logger;
        }

        //[Authorize] to Use Authorization configure Issuer, Audience IssuerSigningKey in Program.cs
        // GET api/<DogsBreedController>/saluki
        [HttpGet("{breed}")]
        public async Task<IActionResult> GetDogBreed(string breed) 
        {
            try
            {
                // Add token authentication
                var breeds = await _breedRepo.GetBreedByNameAsync(breed);
                if (breeds == null)
                {
                    return NotFound();
                }
                
                return Ok(breeds);  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500);
            }
        }

    }
}
