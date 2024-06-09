using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Users;
using SmartEats.Models.Users;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Empresa,RH")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                await _userService.RegisterAsync(createUserDTO);
                return Ok("Usuário Cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível cadastrar o Usuário devido ao erro:\n " + ex.Message);
            }

        }

        [HttpPut("atualizar-senha/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] PasswordChangeDTO passwordChangeDTO)
        {
            try
            {

                var userId = HttpContext.User.FindFirst(c => c.Type == "id")?.Value;
                if (userId == null)
                {
                    return BadRequest("Token Invalido");
                }
                if (userId != id)
                {
                    return BadRequest("Token Invalido");
                }
                var resultado = await _userService.ChangePassword(id, passwordChangeDTO);
                if (resultado)
                {
                    return Ok("Senha Alterada com sucesso!");
                }
                return BadRequest("Não foi possível alterar a senha entre em contato com o administrador");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível Editado o Usuário devido ao erro:\n " + ex.Message);
            }

        }

        [HttpGet("lista-funcionarios-empresa/{idEmpresa}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Empresa,RH")]
        public async Task<IActionResult> ListWorkers(int idEmpresa)
        {
            try
            {

                var companyId = HttpContext.User.FindFirst(c => c.Type == "companyId")?.Value;
                if (companyId == null)
                {
                    return BadRequest("Erro tente mais tarde");
                }
                if (companyId != idEmpresa.ToString())
                {
                    return BadRequest("Token Invalido");
                }

                var resultado = await _userService.ListUsersCompany(idEmpresa);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest("Resultado não encontrado");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível Editado o Usuário devido ao erro:\n " + ex.Message);
            }

        }

        [HttpPut("atualizar/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdatePerfil(string id, [FromBody] EditUserDTO editUserDTO)
        {
            try
            {
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

        [HttpPut("atualizar-usuario/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Empresa,RH")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDTO editUserDTO)
        {
            try
            {
                var companyId = HttpContext.User.FindFirst(c => c.Type == "companyId")?.Value;
                if (companyId == null)
                {
                    return BadRequest("Erro tente mais tarde");
                }
                var user = await _userService.GetUser(id);
                if (user.Id_Company != int.Parse(companyId))
                {
                    return BadRequest("Erro tente mais tarde");
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

        [HttpGet("get-user/{idUser}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Empresa,RH")]
        public async Task<IActionResult> GetUser(string idUser)
        {
            try
            {
                var companyId = HttpContext.User.FindFirst(c => c.Type == "companyId")?.Value;
                if (companyId == null)
                {
                    return BadRequest("Erro tente mais tarde");
                }
                var user = await _userService.GetUser(idUser);
                if (user.Id_Company != int.Parse(companyId))
                {
                    return BadRequest("Erro tente mais tarde");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível recuperar o Usuário devido ao erro:\n " + ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDto)
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

        [HttpPost("logout")]        
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.Logout();
                return Ok("Deslogado com sucesso");
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
