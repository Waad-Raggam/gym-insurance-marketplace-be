using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Services;
using src.Services.Gym;
using static src.DTO.GymDTO;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class GymController : ControllerBase
    {
        private readonly IGymService _gymService;

        public GymController(IGymService gymService)
        {
            _gymService = gymService;
        }

        [HttpPost]
        public async Task<ActionResult<GymReadDto>> CreateOne([FromBody] GymCreateDto createDto)
        {
            var authenticateClaims = HttpContext.User;
            var UserId = authenticateClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (UserId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var userGuid = new Guid(UserId);

            var gymCreated = await _gymService.CreateOnAsync(userGuid, createDto);

            if (gymCreated == null)
            {
                return Conflict("Gym creation failed.");
            }

            return Ok(gymCreated);
        }


        [HttpGet]
        public async Task<ActionResult<List<GymReadDto>>> GetAllGym()
        {
            var gyms = await _gymService.GetAllAsync();

            if (gyms == null || gyms.Count == 0)
            {
                return NotFound("No gyms found.");
            }

            return Ok(gyms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GymReadDto>> GetByIdAsync(Guid id)
        {
            var gym = await _gymService.GetByIdAsync(id);

            if (gym == null)
            {
                return NotFound("Gym not found.");
            }

            return Ok(gym);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<GymReadDto>>> GetByUserIdAsync(Guid userId)
        {
            var gyms = await _gymService.GetByUserIdAsync(userId);

            if (gyms == null || gyms.Count == 0)
            {
                return NotFound("No gyms found for the specified user.");
            }

            return Ok(gyms);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GymReadDto>> UpdateGym(Guid id, [FromBody] GymUpdateDto updateDto)
        {
            var updatedGym = await _gymService.UpdateOneAsync(id, updateDto);

            if (updatedGym == null)
            {
                return NotFound("Gym not found.");
            }

            return Ok(updatedGym);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGym(Guid id)
        {
            var isDeleted = await _gymService.DeleteOneAsync(id);

            if (!isDeleted)
            {
                return NotFound("Gym not found.");
            }

            return Ok("Gym deleted successfully.");
        }
    }
}
