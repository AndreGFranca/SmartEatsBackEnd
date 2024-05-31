using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Users;
using SmartEats.Services.Menus;
using SmartEats.Services.Users;
using System.Data;

namespace SmartEats.Controllers.Menus
{
    [ApiController]
    [Route("menus")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MenuController : StandardController
    {
        readonly MenuService _service;
        public MenuController(MenuService service)
        {
            _service = service;
        }


        [HttpGet("get-all-menus-company")]
        public async Task<IActionResult> GetAllMenusCompany(int companyId)
        {
            try
            {
                DateTime now = DateTime.Now;

                // Início da semana (segunda-feira)
                DateTime startOfWeek = now.AddDays(-(int)now.DayOfWeek + 1);
                if (now.DayOfWeek == DayOfWeek.Sunday)
                {
                    startOfWeek = now.AddDays(-6);
                }

                // Fim da semana (domingo)
                DateTime endOfWeek = startOfWeek.AddDays(6);

                var inicioDaSemana = DateOnly.Parse(now.ToString("yyyy-MM-dd"));
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
