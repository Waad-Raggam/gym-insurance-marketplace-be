using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Services;
using src.Services.Gym;
using src.Services.InsurancePlan;
using static src.DTO.GymDTO;
using static src.DTO.InsurancePlanDTO;
using src.DTO;
using src.Entity;
using src.Repository;
using src.Services;

namespace src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IInsurancePlan _planService;

        public InsurancePlanController(IInsurancePlan planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<ActionResult<List<InsurancePlanReadDto>>> GetAllPlans()
        {
            var plans = await _planService.GetAllInsurancePlansAsync();

            if (plans == null || plans.Count == 0)
            {
                return NotFound("No plans found.");
            }

            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InsurancePlanReadDto>> GetPlanByIdAsync(int id)
        {
            var plan = await _planService.GetInsurancePlanByIdAsync(id);

            if (plan == null)
            {
                return NotFound("plan not found.");
            }

            return Ok(plan);
        }

        [HttpGet("name")]
        public async Task<IActionResult> SearchByCoverage([FromQuery] string coverageType)
        {
            var plans = await _planService.SearchInsurancePlansByCoverageType(coverageType);

            if (plans == null || !plans.Any())
            {
                return NotFound("No insurance plans match the specified coverage.");
            }

            return Ok(plans);
        }


        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<InsurancePlanReadDto>> CreateOnAsync([FromBody] InsurancePlanCreateDto createDto){
            var planCreated = await _planService.CreateInsurancePlanAsync(createDto);
            return Ok(planCreated);
        }

[HttpPut("{id}")]
[Authorize(Roles = "Admin")]
public async Task<ActionResult> UpdatePlanAsync(int id, [FromBody] InsurancePlanUpdateDto updateDto)
{
    var result = await _planService.UpdateInsurancePlanAsync(id, updateDto);
    if (!result) return NotFound("Plan not found.");
    return NoContent();
}

[HttpDelete("{id}")]
// [Authorize(Roles = "Admin")]
public async Task<ActionResult> DeletePlanAsync(int id)
{
    var result = await _planService.DeleteInsurancePlanAsync(id);
    if (!result) return NotFound("Plan not found.");
    return NoContent();
}

    }
}
