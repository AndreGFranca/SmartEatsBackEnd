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

                // Início da semana (segunda-feira)
                DateTime startOfWeek = now.AddDays(-(int)now.DayOfWeek);
                //if (now.DayOfWeek == DayOfWeek.Sunday)
                //{
                //    startOfWeek = now.AddDays(-6);
                //}

                // Fim da semana (domingo)
                DateTime endOfWeek = startOfWeek.AddDays(6);

                var inicioDaSemana = DateOnly.Parse(startOfWeek.ToString("yyyy-MM-dd"));
                var finalDaSemana = DateOnly.Parse(endOfWeek.ToString("yyyy-MM-dd"));

                Console.WriteLine("Start of the week: " + startOfWeek.ToString("yyyy-MM-dd"));
                Console.WriteLine("End of the week: " + endOfWeek.ToString("yyyy-MM-dd"));
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
    }
}
