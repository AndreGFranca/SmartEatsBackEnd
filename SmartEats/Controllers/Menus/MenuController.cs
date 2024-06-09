using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Menus;
using SmartEats.DTOs.Users;
using SmartEats.Services.Menus;
using SmartEats.Services.Users;
using System.Data;

namespace SmartEats.Controllers.Menus
{
    [ApiController]
    [Route("cardapios")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MenuController : StandardController
    {
        readonly MenuService _service;
        public MenuController(MenuService service)
        {
            _service = service;
        }

        [HttpPost("cadastrar/{idEmpresa}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Empresa,Cozinha")]
        public async Task<IActionResult> RegisterNewMenu(int idEmpresa, [FromBody] List<CreateMenuDTO> listCreateMenuDTO)
        {
            try
            {
                Console.WriteLine(listCreateMenuDTO[0].Data.ToString());
                var companyId = HttpContext.User.FindFirst(c => c.Type == "companyId")?.Value;
                if (companyId == null)
                {
                    return BadRequest("Erro tente mais tarde");
                }
                if (companyId != idEmpresa.ToString())
                {
                    return BadRequest("Token Invalido");
                }
                var resultado = await _service.Register(listCreateMenuDTO);
                if (resultado)
                {
                    return Ok("Menu(s) Cadastrado(s) com sucesso");
                }
                return BadRequest("Menu não cadastrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível recuperar o Usuário devido ao erro:\n " + ex.Message);
            }


        }

        [HttpGet("obter-todos-cardapios-empresa/{companyId}")]
        public async Task<IActionResult> GetAllMenusCompany(int companyId)
        {
            try
            {
                DateTime now = DateTime.Now;

                DateTime startOfWeek = now.AddDays(-(int)now.DayOfWeek);

                DateTime endOfWeek = startOfWeek.AddDays(6);

                var inicioDaSemana = DateOnly.Parse(startOfWeek.ToString("yyyy-MM-dd"));
                var finalDaSemana = DateOnly.Parse(endOfWeek.ToString("yyyy-MM-dd"));

                var menuList = await _service.GetAllMenusCompany(companyId, inicioDaSemana, finalDaSemana);

                return Ok(menuList);
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

        [HttpGet("obter-todos-cardapios-funcionario/{companyId}")]
        public async Task<IActionResult> GetAllMenusWorker(int companyId)
        {
            try
            {
                var teste = User.Identity;
                var userId = HttpContext.User.FindFirst(c => c.Type == "id")?.Value;
                if (userId == null)
                {
                    return BadRequest("Token Invalido");
                }
                var empresaId = HttpContext.User.FindFirst(c => c.Type == "companyId")?.Value;
                if (empresaId == null)
                {
                    return BadRequest("Erro tente mais tarde");
                }
                if (companyId.ToString() != empresaId)
                {
                    return BadRequest("Token Invalido");
                }
                DateTime now = DateTime.Now;

                DateTime startOfWeek = now.AddDays(-(int)now.DayOfWeek);

                DateTime endOfWeek = startOfWeek.AddDays(6);

                var inicioDaSemana = DateOnly.Parse(startOfWeek.ToString("yyyy-MM-dd"));
                var finalDaSemana = DateOnly.Parse(endOfWeek.ToString("yyyy-MM-dd"));

                var menuList = await _service.GetAllMenusWorker(companyId, inicioDaSemana, finalDaSemana, userId);

                return Ok(menuList);
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
