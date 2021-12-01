using BlackRiver.Data.Services;
using BlackRiver.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlackRiver.API.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserService userService = new();

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Register([FromBody] UserLogin login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password) || login.Password.Length < 8)
                    return BadRequest(-1);

                var user = (await DataServices.UserLoginService.GetAll()).FirstOrDefault(l => l.Username.Equals(login.Username));

                if (user != null)
                    return BadRequest(0);

                var result = await DataServices.UserLoginService.Create(login);

                if (result == null)
                    return BadRequest(-2);

                return Ok(result.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("update")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> UpdateCredentials(string username, string password)
        {
            if (await userService.Update(username, password))
                return Ok();

            return NotFound(new { message = "Usuário ou senha inválidos" });
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(string username, string password)
        {
            var user = await userService.Authenticate(username, password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            return new { user, token };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => string.Format("Usuário Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("authuser")]
        [Authorize]
        public Task<UserLogin> GetRole() => userService.GetAuthUser(User.Identity.Name);

        [HttpGet]
        [Route("funcionario")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Você tem permissões de Funcionário";

        [HttpGet]
        [Route("gerente")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Você tem permissões de Gerente";
    }
}
