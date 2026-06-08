using FluxoDeTrabalhoAgil.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeTrabalhoAgil.Api.Controllers
{
    [ApiController]
    [Route("api/ia")]
    public class IAController : ControllerBase
    {
        private readonly IAService _iaService;

        public IAController(IAService iaService)
        {
            _iaService = iaService;
        }

        [HttpPost("resumo")]
        public async Task<IActionResult> GerarResumo([FromBody] string texto)
        {
            var resumo = await _iaService.GerarResumo(texto);

            return Ok(resumo);
        }
    }
}