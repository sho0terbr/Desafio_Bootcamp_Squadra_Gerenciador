using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gerenciador.Context;
using Gerenciador.Models;
using Gerenciador.Enums;
using Microsoft.AspNetCore.Authorization;
using Gerenciador.DTOs;

namespace Gerenciador.Controllers
{
    [Route("cursos")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly GerenciadorDbContext _context;

        public CursosController(GerenciadorDbContext context)
        {
            _context = context;
        }

        // GET: api/CursoModels
        [HttpGet]
        [Route("busca_todos")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CursoModel>>> GetCursos()
        {
            var testes = await _context.Cursos.ToListAsync();

            return testes;
        }

        [HttpGet]
        [Route("busca_por_status")]
        [AllowAnonymous]

        public async Task<ActionResult<IEnumerable<CursoModel>>> GetStatus(StatusCursoEnum status)
        {


            var cursoStatus = await _context.Cursos.Where(c => c.Status.Equals(status)).ToListAsync();
            

            return cursoStatus;

        }

        // GET: api/CursoModels/5
        [HttpGet]
        [Route("buscar_por_id")]
        [AllowAnonymous]
        public async Task<ActionResult<CursoModel>> GetCursoModel(int id)
        {
            var cursoModel = await _context.Cursos.FindAsync(id);

            if (cursoModel == null)
            {
                return NotFound(new { message = "Id Do Curso Inexistente" });
            }

            return cursoModel;
        }

        // PUT: api/CursoModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("mudar_status")]
        [Authorize(Roles = "Gerente,Secretaria,Secretario")]
        public async Task<IActionResult> PutCursoModel(int id, MudarStatusDto mudarStatusDto)

        {
           

            var cursoModel = new CursoModel();

            cursoModel.Status = mudarStatusDto.Status;
            cursoModel.Id = id;

            
            _context.Entry(cursoModel).Property(p => p.Status).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoModelExists(id))
                {
                    return NotFound(new { message = "Id Do Curso Inexistente" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CursoModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("criar")]
        [Authorize(Roles = "Gerente,Secretaria,Secretario")]
        public async Task<ActionResult<CursoModel>> PostCursoModel(CursoModel cursoModel)
        {
            _context.Cursos.Add(cursoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCursoModel", new { id = cursoModel.Id }, cursoModel);
        }

        // DELETE: api/CursoModels/5
        [HttpDelete]
        [Route("deletar")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> DeleteCursoModel(int id)
        {
            var cursoModel = await _context.Cursos.FindAsync(id);
            if (cursoModel == null)
            {
                return NotFound(new { message = "Id Inválido" });
            }

            _context.Cursos.Remove(cursoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoModelExists(int id)
        {
            return _context.Cursos.Any(e => e.Id == id);
        }

        
    }
}
