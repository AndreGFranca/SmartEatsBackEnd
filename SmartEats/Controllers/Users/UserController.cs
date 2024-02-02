using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Users;
using SmartEats.Services.Users;

namespace SmartEats.Controllers.Users
{
    [ApiController]
    [Route("usuarios")]
    public class UserController : StandardController
    {
        private UserService _userService;

        public UserController(UserService cadastroService)
        {
            _userService = cadastroService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                await _userService.RegisterAsync(createUserDTO);
                return Ok("Usuário Cadastrado com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível cadastrar o Usuário devido ao erro:\n "+ ex.Message);
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginDto)
        {
            try
            {
                var token = await _userService.Login(loginDto);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }
    }
}
