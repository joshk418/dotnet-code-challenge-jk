using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger<CompensationController> _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpGet("employee/{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(string id)
        {
            _logger.LogDebug($"Received employee compensation get request for '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);
            if (compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Compensation compensation)
        {
            if (compensation.Employee == null)
            {
                _logger.LogDebug("Employee compensation creation failed due to invalid employee.");
                return BadRequest("Employee is invalid");
            }
            
            _logger.LogDebug($"Received employee compensation create request'{compensation.Employee.FirstName} {compensation.Employee.LastName}'");

            _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = compensation.Employee.EmployeeId }, compensation);
        }
    }
}