using FluxoDeTrabalhoAgil.Api.Data;
using FluxoDeTrabalhoAgil.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeTrabalhoAgil.Api.Controllers
{
    [ApiController]
    [Route("api/reunioes")]
    public class ReunioesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReunioesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reunioes = await _context.Reunioes.ToListAsync();
            return Ok(reunioes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reuniao = await _context.Reunioes.FindAsync(id);

            if (reuniao == null)
                return NotFound();

            return Ok(reuniao);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reuniao reuniao)
        {
            reuniao.Data = DateTime.Now;

            _context.Reunioes.Add(reuniao);
            await _context.SaveChangesAsync();

            return Ok(reuniao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reuniao = await _context.Reunioes.FindAsync(id);

            if (reuniao == null)
                return NotFound();

            _context.Reunioes.Remove(reuniao);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}