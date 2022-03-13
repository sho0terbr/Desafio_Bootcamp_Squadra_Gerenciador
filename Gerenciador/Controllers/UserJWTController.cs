using System.Threading.Tasks;
using Gerenciador.Context;
using Gerenciador.DTOs;
using Gerenciador.Models;
using Gerenciador.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Controllers
{
    [Route("jwt")]
    [ApiController]
    public class UserJWTController : ControllerBase
    {
        private readonly GerenciadorDbContext _context;

        public UserJWTController(GerenciadorDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("gerar_token")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserTokenDto User)
        {

            var user = await GetUser(User.Usuario, User.Senha);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Senha = "";
            return new
            {
                user = user,
                token = token
            };
        }

        private async Task<UserModel> GetUser(string usuario, string senha)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Usuario == usuario && u.Senha == senha);
        }

    }
}
