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
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
          if (_context.Usuario == null)
          {
              return NotFound();
          }
            return await _context.Usuario.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        { 

            if (_context.Usuario == null)
          {
              return NotFound();
          }
          var usuario = await _context.Usuario.FindAsync(id);
           

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }


       


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(Guid id, UsuarioDTO usuarioDTO)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }
            usuario.Nome = usuarioDTO.Nome;
            usuario.Email = usuarioDTO.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDTO usuarioDTO)
        {
          if (_context.Usuario == null)
          {
              return Problem("Entity set 'DataContext.Usuario'  is null.");
          }

            var usuario = new Usuario {
                Id = new Guid(),
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Cafeterias = new List<CafeteriaC>() // CRIAR A LISTA DE CAFETERIAS VAZIAS POR CONTA DO JSON IGNORE
            };

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            if (_context.Usuario == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(Guid id)
        {
            return (_context.Usuario?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost("/avaliacao")]
        public async Task<ActionResult<Avaliacao>> PostAvaliacao(AvaliacaoDTO avaliacaoDTO)
        {
            var usuario = await _context.Usuario.FindAsync(avaliacaoDTO.UsuarioId);
            var cafeteria = await _context.CafeteriaC.FindAsync(avaliacaoDTO.CafeteriaId);


            if (_context.CafeteriaC == null)
            {
                return Problem("Esta cafeteria nao existe");
            }

            if (avaliacaoDTO.Nota < 0 || avaliacaoDTO.Nota > 10)
            {
                return Problem ("A nota só pode ser entre 0 e 10");
            }

            var avaliacao = new Avaliacao
            {
                Nota = avaliacaoDTO.Nota,
                Comentario = avaliacaoDTO.Comentario,
                Cafeterias = cafeteria,
                Usuarios = usuario
            };

            _context.Avaliacao.Add(avaliacao);
            await _context.SaveChangesAsync();

            return avaliacao;
        }

        [HttpPost("/Evento")]
        public async Task<ActionResult<Evento>> PostEvento(EventoDTO eventoDTO)
        {
            var usuario = await _context.Usuario.FindAsync(eventoDTO.UsuarioId);
            var cafeteria = await _context.CafeteriaC.FindAsync(eventoDTO.CafeteriaId);


            if (_context.CafeteriaC == null)
            {
                return Problem("Esta cafeteria nao existe");
            }


            var evento = new Evento
            {
                Nome = eventoDTO.Nome,
                Data = eventoDTO.Data,
                Cafeterias = cafeteria,
                Usuarios = usuario
            };

            _context.Evento.Add(evento);
            await _context.SaveChangesAsync();

            return evento;
        }

        [Route("listar-cafeterias-do-usuario/{idUsuario}")]
        [HttpGet]
        //public async Task<ActionResult<List<Item>>> GetAllItemsOfAProduct(Guid PedidoId)
        public async Task<ActionResult<List<CafeteriaC>>> ListarTodasAsCafeteriasCadastradasPeloUsuario(Guid idUsuario)
        {
            if (_context.CafeteriaC == null)
            {
                return NotFound();
            }

            var cafeteriasDoUsuario = await _context.CafeteriaC.Where(x => x.usuario.Id == idUsuario).ToListAsync();

            return cafeteriasDoUsuario;
        }


        [Route("listar-avaliacoes-do-usuario/{idUsuario}")]
        [HttpGet]
        //public async Task<ActionResult<List<Item>>> GetAllItemsOfAProduct(Guid PedidoId)
        public async Task<ActionResult<List<Avaliacao>>> ListarTodasAsAvaliacoesCadastradasPeloUsuario(Guid idUsuario)
        {
            if (_context.Avaliacao == null)
            {
                return NotFound();
            }

            var AvaliacoesDoUsuario = await _context.Avaliacao.Where(x => x.Usuarios.Id == idUsuario).ToListAsync();

            return AvaliacoesDoUsuario;
        }


        [Route("listar-eventos-do-usuario/{idUsuario}")]
        [HttpGet]
        //public async Task<ActionResult<List<Item>>> GetAllItemsOfAProduct(Guid PedidoId)
        public async Task<ActionResult<List<Evento>>> ListarTodosEventosCadastradasPeloUsuario(Guid idUsuario)
        {
            if (_context.Evento == null)
            {
                return NotFound();
            }

            var EventosdoUsuario = await _context.Evento.Where(x => x.Usuarios.Id == idUsuario).ToListAsync();

            return EventosdoUsuario;
        }






    }
}
