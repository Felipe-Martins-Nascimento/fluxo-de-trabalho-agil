using FluxoDeTrabalhoAgil.Api.Data;
using FluxoDeTrabalhoAgil.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeTrabalhoAgil.Api.Controllers
{
    [ApiController]
    [Route("api/review")]
    public class SprintReviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SprintReviewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dados =
                await _context.SprintReviews
                    .Include(x => x.Entregas)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

            return Ok(dados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review =
                await _context.SprintReviews
                    .Include(x => x.Entregas)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] SprintReview review)
        {
            if (review == null)
                return BadRequest("Review nula");

            if (string.IsNullOrWhiteSpace(
                review.NomeSprint))
            {
                return BadRequest("Nome da Sprint é obrigatório");
            }
            review.DataCriacao = DateTime.Now;

            review.Entregas ??= new();

            foreach (var item in review.Entregas)
            {
                item.Id = 0;
            }

            _context.SprintReviews.Add(review);

            await _context.SaveChangesAsync();

            var reviewCriada = await _context.SprintReviews
                                .Include(x => x.Entregas)
                                .FirstOrDefaultAsync(x => x.Id == review.Id
        );

            return Ok(reviewCriada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] SprintReview model)
        {
            var review = await _context.SprintReviews
                            .Include(x => x.Entregas)
                            .FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
                return NotFound();

            review.NomeSprint = model.NomeSprint;

            review.TasksTotais = model.TasksTotais;
            review.TasksConcluidas = model.TasksConcluidas;
            review.TasksNaoConcluidas = model.TasksNaoConcluidas;

            review.VelocityEntregue = model.VelocityEntregue;
            review.VelocityNaoEntregue = model.VelocityNaoEntregue;

            review.Participantes = model.Participantes;

            review.BugsCriticos = model.BugsCriticos;
            review.BugsMedios = model.BugsMedios;
            review.BugsBaixos = model.BugsBaixos;
            review.BugsCorrigidos = model.BugsCorrigidos;
            review.BugsPendentes = model.BugsPendentes;
            review.BugsEncontrados = model.BugsEncontrados;

            review.StoryPointsPlanejados = model.StoryPointsPlanejados;
            review.StoryPointsEntregues = model.StoryPointsEntregues;

            review.EficienciaSprint = model.EficienciaSprint;
            review.TaxaSucesso = model.TaxaSucesso;
            review.TempoMedioTask = model.TempoMedioTask;
            review.ParticipacaoTime = model.ParticipacaoTime;

            review.PontosPositivos = model.PontosPositivos;
            review.PontosMelhoria = model.PontosMelhoria;
            review.FeedbackScrumMaster = model.FeedbackScrumMaster;
            review.FeedbackProductOwner = model.FeedbackProductOwner;
            review.ProximosPassos = model.ProximosPassos;
            review.DecisoesTomadas = model.DecisoesTomadas;

            _context.EntregasReview.RemoveRange(review.Entregas);

            review.Entregas = new();

            foreach (var item in model.Entregas)
            {
                review.Entregas.Add(
                    new EntregaReview
                    {
                        Nome = item.Nome,
                        Status = item.Status
                    });
            }

            await _context.SaveChangesAsync();

            var reviewAtualizada = await _context.SprintReviews
                                    .Include(x => x.Entregas)
                                    .FirstOrDefaultAsync(x => x.Id == review.Id
        );

            return Ok(reviewAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var review = await _context.SprintReviews
                            .Include(x => x.Entregas)
                            .FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
                return NotFound();

            _context.EntregasReview.RemoveRange(
                review.Entregas);

            _context.SprintReviews.Remove(review);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}