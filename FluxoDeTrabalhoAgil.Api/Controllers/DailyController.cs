using FluxoDeTrabalhoAgil.Api.Data;
using FluxoDeTrabalhoAgil.Api.Models;
using FluxoDeTrabalhoAgil.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeTrabalhoAgil.Api.Controllers
{
    [ApiController]
    [Route("api/daily")]
    public class DailyController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAService _iaService;

        public DailyController(AppDbContext context, IAService iaService)
        {
            _context = context;
            _iaService = iaService;
        }

        [HttpGet]
        public async Task<IActionResult> Buscar()
        {
            var mensagens =
                await _context.MensagensDaily
                .OrderByDescending(x => x.Data)
                .ToListAsync();

            return Ok(mensagens);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(MensagemDaily mensagem)
        {
            mensagem.Data = DateTime.Now;
            _context.MensagensDaily.Add(mensagem);

            await _context.SaveChangesAsync();
            return Ok(mensagem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] string novoTexto)
        {
            var mensagem = await _context.MensagensDaily.FindAsync(id);

            if (mensagem == null)
                return NotFound();

            mensagem.Texto = novoTexto;

            await _context.SaveChangesAsync();

            return Ok(mensagem);
        }

        [HttpPut("{id}/nota")]
        public async Task<IActionResult> AtualizarNota(int id, [FromBody] int nota)
        {
            var mensagem = await _context.MensagensDaily.FindAsync(id);

            if (mensagem == null)
                return NotFound();

            mensagem.Nota = nota;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}/resposta")]
        public async Task<IActionResult> Responder(int id, [FromBody] RespostaRequest request)
        {
            var mensagem =
                await _context.MensagensDaily
                .FindAsync(id);

            if (mensagem == null)
                return NotFound();

            var respostas =
                mensagem.Respostas
                ?? new List<RespostaDaily>();

            respostas.Add(
                new RespostaDaily
                {
                    Usuario = request.Usuario,
                    Texto = request.Texto,
                    Data = DateTime.Now
                });

            mensagem.Respostas = respostas;

            _context.Entry(mensagem)
                .Property(x => x.Respostas)
                .IsModified = true;

            await _context.SaveChangesAsync();

            return Ok(mensagem);
        }

        [HttpPost("resumo")]
        public async Task<IActionResult> GerarResumo([FromBody] string texto)
        {
            try
            {
                var resumo = await _iaService.GerarResumo(texto);

                return Ok(resumo);
            }
            catch
            {
                return Ok("IA offline. Abra o Ollama para gerar resumo.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var mensagem = await _context.MensagensDaily.FindAsync(id);

            if (mensagem == null)
                return NotFound();

            _context.MensagensDaily.Remove(mensagem);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}