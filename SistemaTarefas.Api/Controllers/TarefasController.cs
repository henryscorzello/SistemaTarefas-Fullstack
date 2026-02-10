using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaTarefas.Api.Data;
using SistemaTarefas.Shared;

namespace SistemaTarefas.Api.Controllers
{
    [Route("api/[controller]")] // Define a rota como /api/tarefas
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injeção de Dependência: traz o banco de dados para dentro do controller
        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        // READ: Listar todas as tarefas (GET /api/tarefas)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            return await _context.Tarefas.ToListAsync();
        }

        // CREATE: Adicionar nova tarefa (POST /api/tarefas)
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTarefas), new { id = tarefa.Id }, tarefa);
        }

        // UPDATE: Alterar uma tarefa existente (PUT /api/tarefas/{id})
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id) return BadRequest();

            _context.Entry(tarefa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: Remover uma tarefa (DELETE /api/tarefas/{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return NotFound();

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}