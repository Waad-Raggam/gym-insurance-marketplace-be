using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Repository;
using src.Services;

namespace src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GymInsuranceController : ControllerBase
    {
        private readonly IGymInsuranceService _gymInsuranceService;

        public GymInsuranceController(IGymInsuranceService gymInsuranceService)
        {
            _gymInsuranceService = gymInsuranceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GymInsuranceReadDto>>> GetAllGymInsurances()
        {
            var gymInsurances = await _gymInsuranceService.GetAllAsync();

            if (gymInsurances == null || gymInsurances.Count == 0)
            {
                return NotFound("No gym insurances found.");
            }

            return Ok(gymInsurances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymInsuranceReadDto>> GetGymInsurance(Guid id)
        {
            var gymInsurance = await _gymInsuranceService.GetByIdAsync(id);

            if (gymInsurance == null)
            {
                return NotFound("Gym insurance not found.");
            }

            return Ok(gymInsurance);
        }

        [HttpPost]
        public async Task<ActionResult<GymInsuranceReadDto>> CreateGymInsurance([FromBody] GymInsuranceCreateDto createDto)
        {
            var gymInsuranceCreated = await _gymInsuranceService.CreateOnAsync(createDto);

            if (gymInsuranceCreated == null)
            {
                return Conflict("Gym insurance creation failed.");
            }

            return Ok(gymInsuranceCreated);
        }

        [HttpPut("{id}")]
        public async  Task<ActionResult<GymInsuranceReadDto>> UpdateGymInsurance(Guid id, GymInsuranceUpdateDto updateDto)
        {
            var updatedGymInsurance = await _gymInsuranceService.UpdateOnAsync(id, updateDto);

            if (updatedGymInsurance == null)
            {
                return NotFound("Gym insurance not found.");
            }

            return Ok(updatedGymInsurance);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGymInsurance(Guid id)
        {
            var isDeleted = await _gymInsuranceService.DeleteOneAsync(id);

            if (!isDeleted)
            {
                return NotFound("Gym insurance not found.");
            }

            return Ok("Gym insurance deleted successfully.");
        }
    }
}
