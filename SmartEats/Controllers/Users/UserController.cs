using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Users;
using SmartEats.Services.Users;
using System.Security.Claims;

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

        //[Authorize(policy: "AdministradorPolicy")]
        
        [HttpPost("teste")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Empresa")]
        public async Task<IActionResult> Teste()
        {
            try
            { 
                return Ok("Usuário Cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível cadastrar o Usuário devido ao erro:\n " + ex.Message);
            }

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

        [HttpPut("atualizar/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateUser(string id, EditUserDTO editUserDTO)
        {
            try
            {
                //Faça um código para pegar o id que vem no token do header.    

                var userId = User.FindFirst(c => c.Type == "id")?.Value;
                if (userId == null)
                {
                    return BadRequest("Token Invalido");
                }                
                if (userId != id)
                {
                    return BadRequest("Token Invalido");
                }
                var resultado = await _userService.UpdateUser(id, editUserDTO);
                if (resultado)
                {
                    return Ok("Usuário Editado com sucesso!");
                }
                return BadRequest("Não foi possível Editado o Usuário. Contate o administrador");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível Editado o Usuário devido ao erro:\n " + ex.Message);
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
