using FluxoDeTrabalhoAgil.Api.Data;
using FluxoDeTrabalhoAgil.Api.Models;
using FluxoDeTrabalhoAgil.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeTrabalhoAgil.Api.Controllers
{
    [ApiController]
    [Route("api/retro")]
    public class RetroController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAService _iaService;

        public RetroController(AppDbContext context, IAService iaService)
        {
            _context = context;
            _iaService = iaService;
        }

        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            var sprints =
                await _context.SprintRetros
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return Ok(sprints);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var sprint = await _context.SprintRetros
                            .Include(x => x.Cards)
                            .FirstOrDefaultAsync(x => x.Id == id);

            if (sprint == null)
                return NotFound();

            return Ok(sprint);
        }

        [HttpPost]
        public async Task<IActionResult> Salvar([FromBody] RetroSalvarRequest request)
        {
            if (request == null)
                return BadRequest();

            SprintRetro sprint;

            if (request.Id == 0)
            {
                sprint = new SprintRetro
                {
                    NomeSprint = request.NomeSprint,
                    DataCriacao = DateTime.Now
                };

                foreach (var card in request.Cards)
                {
                    sprint.Cards.Add(
                        new RetroCard
                        {
                            Tipo = card.Tipo,
                            Texto = card.Texto,
                            Votos = card.Votos,
                            DataCriacao = DateTime.Now
                        });
                }

                _context.SprintRetros.Add(sprint);
            }
            else
            {
                sprint = await _context.SprintRetros
                    .Include(x => x.Cards)
                    .FirstOrDefaultAsync(x =>
                        x.Id == request.Id);

                if (sprint == null)
                    return NotFound();

                sprint.NomeSprint =
                    request.NomeSprint;

                _context.RetroCards.RemoveRange(
                    sprint.Cards);

                sprint.Cards.Clear();

                foreach (var card in request.Cards)
                {
                    sprint.Cards.Add(
                        new RetroCard
                        {
                            Tipo = card.Tipo,
                            Texto = card.Texto,
                            Votos = card.Votos,
                            DataCriacao = DateTime.Now
                        });
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                sprint.Id,
                sprint.NomeSprint
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var sprint = await _context.SprintRetros
                            .Include(x => x.Cards)
                            .FirstOrDefaultAsync(x => x.Id == id);

            if (sprint == null)
                return NotFound();

            _context.SprintRetros
                .Remove(sprint);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("gerar-insights")]
        public async Task<IActionResult> GerarInsights(InsightsRequest request)
        {
            try
            {
                var resultado =
                    await _iaService
                    .GerarInsightsRetro(
                        request.Positivos,
                        request.Negativos,
                        request.Sugestoes);

                return Ok(resultado);
            }
            catch
            {
                return Ok("IA offline. Abra o Ollama.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] SprintRetro sprint)
        {
            var existente = await _context.SprintRetros
                            .Include(x => x.Cards)
                            .FirstOrDefaultAsync(x => x.Id == id);

            if (existente == null)
                return NotFound();

            existente.NomeSprint =
                sprint.NomeSprint;

            existente.Cards.Clear();

            foreach (var card in sprint.Cards)
            {
                existente.Cards.Add(
                    new RetroCard
                    {
                        Tipo = card.Tipo,
                        Texto = card.Texto,
                        Votos = card.Votos,
                        DataCriacao = DateTime.Now
                    });
            }

            await _context.SaveChangesAsync();

            return Ok(existente);
        }
    }
}