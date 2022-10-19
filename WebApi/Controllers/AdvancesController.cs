using Business.Abstract;
using Entities.Dtos.Advance;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvancesController : ControllerBase
    {
        private readonly IAdvanceService _advanceService;

        public AdvancesController(IAdvanceService advanceService)
        {
            _advanceService = advanceService;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] AdvanceGetByIdRequestDto requestDto)
        {
            var result = await _advanceService.GetByIdAsync(requestDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromForm] AdvanceAddDto addDto)
        {
            var result = await _advanceService.AddAsync(addDto);
            return StatusCode(result.StatusCode, result);
        }
    }
}
