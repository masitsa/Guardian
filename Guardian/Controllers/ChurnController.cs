using Guardian.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Guardian.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ChurnController : ControllerBase
    {

        private readonly ILogger<ChurnController> _logger;

        public ChurnController(ILogger<ChurnController> logger)
        {
            this._logger = logger;
        }

        [HttpPost("Usage")]
        public async Task<ActionResult<double>> PostUsage([FromBody] Statistics statistics)
        {
            try
            {
                var usage = await MawinguMlService.GetUsage(statistics).ConfigureAwait(false);

                return Ok(usage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Probability")]
        public async Task<ActionResult<double>> PostProbability([FromBody] Probability probability)
        {
            try
            {
                var probabilityPercentage = await MawinguMlService.GetProbability(probability).ConfigureAwait(false);

                return Ok(probabilityPercentage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}