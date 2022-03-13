using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gerenciador.Context;
using Gerenciador.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gerenciador.Controllers
{
    [Route("usuarios")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly GerenciadorDbContext _context;

        public UsersController(GerenciadorDbContext context)
        {
            _context = context;
        }

        // GET: api/UserModels
        [HttpGet]
        [Route("busca_todos")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserModels/5
        [HttpGet]
        [Route("busca_por_id")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> GetUserModel(int id)
        {
            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return userModel;
        }

        // PUT: api/UserModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("alterar_dados")]
        [AllowAnonymous]
        public async Task<IActionResult> PutUserModel(int id, UserModel userModel)
        {
            userModel.Id = id;
            _context.Entry(userModel).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserModels

        [HttpPost]
        [Route("criar")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> PostUserModel(UserModel userModel)
        {
            _context.Users.Add(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserModel", new { id = userModel.Id }, userModel);
        }

        // DELETE: api/UserModels/5
        [HttpDelete]
        [Route("deletar")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUserModel(int id)
        {
            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
