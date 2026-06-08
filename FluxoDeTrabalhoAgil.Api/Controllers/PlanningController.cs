using FluxoDeTrabalhoAgil.Api.Data;
using FluxoDeTrabalhoAgil.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeTrabalhoAgil.Api.Controllers
{
    [ApiController]
    [Route("api/planning")]
    public class PlanningController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanningController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dados =
                await _context.SprintPlannings
                    .Include(x => x.Backlog)
                    .OrderByDescending(x => x.Id)
                    .ToListAsync();

            return Ok(dados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sprint =
                await _context.SprintPlannings
                    .Include(x => x.Backlog)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (sprint == null)
                return NotFound();

            return Ok(sprint);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] SprintPlanning sprint)
        {
            sprint.DataCriacao = DateTime.Now;

            _context.SprintPlannings.Add(sprint);

            foreach (var item in sprint.Backlog)
            {
                item.Id = 0;
            }

            await _context.SaveChangesAsync();

            return Ok(sprint);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] SprintPlanning model)
        {
            var sprint =
                await _context.SprintPlannings
                    .Include(x => x.Backlog)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (sprint == null)
                return NotFound();

            sprint.NomeSprint = model.NomeSprint;
            sprint.MetaSprint = model.MetaSprint;
            sprint.QtdDevs = model.QtdDevs;
            sprint.HorasDev = model.HorasDev;
            sprint.Velocity = model.Velocity;

            _context.SprintTasks.RemoveRange(sprint.Backlog);

            sprint.Backlog = new List<SprintTask>();

            foreach (var item in model.Backlog)
            {
                sprint.Backlog.Add(new SprintTask
                {
                    Nome = item.Nome,
                    Pontos = item.Pontos,
                    Prioridade = item.Prioridade,
                    NaSprint = item.NaSprint
                });
            }

            await _context.SaveChangesAsync();

            return Ok(sprint);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var sprint =
                await _context.SprintPlannings
                    .Include(x => x.Backlog)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (sprint == null)
                return NotFound();

            _context.SprintTasks.RemoveRange(sprint.Backlog);
            _context.SprintPlannings.Remove(sprint);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}