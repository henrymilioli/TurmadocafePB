using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Models;
using Cafeteria.DTO;

namespace Cafeteria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeteriaCController : ControllerBase
    {
        private readonly DataContext _context;

        public CafeteriaCController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CafeteriaC
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CafeteriaC>>> GetCafeteriaC()
        {
          if (_context.CafeteriaC == null)
          {
              return NotFound();
          }
            return await _context.CafeteriaC.ToListAsync();
        }

        // GET: api/CafeteriaC/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CafeteriaC>> GetCafeteriaC(Guid id)
        {
          if (_context.CafeteriaC == null)
          {
              return NotFound();
          }
             var cafeteriaC = await _context.CafeteriaC.FindAsync(id);
           

            if (cafeteriaC == null)
            {
                return NotFound();
            }

            return cafeteriaC;
        }

        // PUT: api/CafeteriaC/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCafeteriaC(Guid id, EditarCafeteriaCDTO editarCafeteriaCDTO)
        {
            var cafeteria = await _context.CafeteriaC.FindAsync(id);

            if (cafeteria == null)
            {
                return NotFound();
            }

            cafeteria.Nome = editarCafeteriaCDTO.Nome;
            cafeteria.Endereco = editarCafeteriaCDTO.Endereco;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CafeteriaCExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Atualização realizada com sucesso" });
        }

        // POST: api/CafeteriaC
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CafeteriaC>> PostCafeteriaC(CafeteriaCDTO cafeteriaCDTO)
        {
            var usuario = await _context.Usuario.FindAsync(cafeteriaCDTO.UsuarioId);

            if (_context.CafeteriaC == null)
          {
              return Problem("Entity set 'DataContext.CafeteriaC'  is null.");
          }

            var cafeteriaExistente = await _context.CafeteriaC.FirstOrDefaultAsync(x => x.Endereco == cafeteriaCDTO.Endereco);

            if (cafeteriaExistente != null) 
            {
                return BadRequest($"Uma cafeteria com o MESMO endereço já existe: {cafeteriaCDTO.Endereco}");
            }

            var cafeteria = new CafeteriaC
            {
                Id = new Guid(),
                Nome = cafeteriaCDTO.Nome,
                Endereco = cafeteriaCDTO.Endereco,
                usuario = usuario
            };

            _context.CafeteriaC.Add(cafeteria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCafeteriaC", new { id = cafeteria.Id }, cafeteria);
        }

        // DELETE: api/CafeteriaC/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafeteriaC(Guid id)
        {
            if (_context.CafeteriaC == null)
            {
                return NotFound();
            }
            var cafeteriaC = await _context.CafeteriaC.FindAsync(id);
            if (cafeteriaC == null)
            {
                return NotFound();
            }

            _context.CafeteriaC.Remove(cafeteriaC);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CafeteriaCExists(Guid id)
        {
            return (_context.CafeteriaC?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [Route("listar-avaliacoes-da-cafeteria/{idCafeteria}")]
        [HttpGet]
        public async Task<ActionResult<List<Avaliacao>>> ListarTodasAsAvaliacoesCadastradasNaCafeteria(Guid idCafeteria)
        {
            if (_context.Avaliacao == null)
            {
                return NotFound();
            }

            var AvaliacoesDaCafeteria = await _context.Avaliacao.Where(x => x.Cafeterias.Id == idCafeteria).ToListAsync();

            return AvaliacoesDaCafeteria;
        }


        [Route("listar-eventos-da-Cafeteria/{idCafeteria}")]
        [HttpGet]
        //public async Task<ActionResult<List<Item>>> GetAllItemsOfAProduct(Guid PedidoId)
        public async Task<ActionResult<List<Evento>>> ListarTodosEventosCadastradosNaCafeteria(Guid idCafeteria)
        {
            if (_context.Evento == null)
            {
                return NotFound();
            }

            var EventosCafeteria = await _context.Evento.Where(x => x.Cafeterias.Id == idCafeteria).ToListAsync();

            return EventosCafeteria;
        }
    }
}
