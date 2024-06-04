using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Confirms;
using SmartEats.Services.Confirms;

namespace SmartEats.Controllers.Confirms
{
    [ApiController]
    [Route("confirmacoes")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ConfirmController : StandardController
    {
        readonly ConfirmService _confirmService;
        public ConfirmController(ConfirmService confirmService)
        {
            _confirmService = confirmService;
        }
        [HttpPost("confirmar-presenca")]
        public async Task<IActionResult> ConfirmPresence(List<CreateConfirmDTO> listCreateConfirmDTO)
        {
            try
            {
                var resultado = await _confirmService.RegisterConfirms(listCreateConfirmDTO);
                if (true)
                {
                    return Ok("Presença(s) Confirmada com sucesso");
                }
                return BadRequest("Presença(s) não cadastrado");

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }
        [HttpGet("obter-horarios-disponiveis")]
        public async Task<IActionResult> GetAvailableTimes()
        {
            try
            {
                var resultado = _confirmService.GetAvailableTimes();
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return Ok(new List<TimeOnly>());

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }
        [HttpGet("obter-contagem-horarios/{idEmpresa}")]
        public async Task<IActionResult> GetCountTimes(int idEmpresa)
        {
            try
            {
                var resultado = _confirmService.GetAvailableTimes();
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return Ok(new List<TimeOnly>());

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }
    }
}
