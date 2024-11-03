using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.DTO;
using src.Entity;
using src.Services;
using src.Services.Gym;
using src.Services.InsurancePlan;
using static src.DTO.GymDTO;
using static src.DTO.InsurancePlanDTO;

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
    }
}
