using Investiment.Domain.Entities;
using Investiment.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly ILogger<InvestmentController> _logger;
        private readonly ICdbService _cdbService;

        public InvestmentController(ILogger<InvestmentController> logger, ICdbService cdbService)
        {
            _logger = logger;
            _cdbService = cdbService;
        }

        [HttpPost("CalculateCDB")]
        public async Task<ActionResult<InvestmentResponse>> CalculateCDB([FromBody] InvestmentRequest request)
        {
            try
            {
                _logger.LogInformation("Calculando CDB...");

                return Ok(await _cdbService.CalculateInvestment(request));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Erro ao calcular CDB...");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}