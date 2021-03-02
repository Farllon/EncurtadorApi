using System;
using System.Threading.Tasks;
using Encurtador.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Encurtador.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository,
                              ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserDto model)
        {
            var user = await _userRepository.GetByCred(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha incorretos!" });

            var token = _tokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost]
        [Route("singup")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Singup([FromBody] UserDto model)
        {
            await _userRepository.Create(new Models.User { Id = Guid.NewGuid(), Username = model.Username, Password = model.Password, Role = "user" });

            return new
            {
                user = model
            };
        }
    }
}