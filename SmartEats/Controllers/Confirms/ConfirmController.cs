using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Confirms;
using SmartEats.Services.Confirms;
using SmartEats.Services.Users;

namespace SmartEats.Controllers.Confirms
{
    [ApiController]
    [Route("confirmacoes")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ConfirmController : StandardController
    {
        private readonly ConfirmService _confirmService;
        private readonly UserService _userService;
        public ConfirmController(ConfirmService confirmService,UserService userService)
        {
            _confirmService = confirmService;
            _userService = userService;
        }
        [HttpPost("confirmar-presenca")]
        public async Task<IActionResult> ConfirmPresence(List<CreateConfirmDTO> listCreateConfirmDTO)
        {
            try
            {
                var resultado = await _confirmService.RegisterConfirms(listCreateConfirmDTO);
                if (resultado)
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
                var resultado = await _confirmService.GetCountTimes(idEmpresa);
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


        [HttpPut("confirmar-comparecimento/{idFuncionario}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Empresa,Cozinha")]
        public async Task<IActionResult> ConfirmPresenceWorker(string idFuncionario, ConfirmPresenceDTO confirm)
        {
            try
            {

                var companyId = HttpContext.User.FindFirst(c => c.Type == "companyId")?.Value;
                var user = await _userService.GetUser(idFuncionario);
                if (companyId == null)
                {
                    return BadRequest("Houve um erro na hora de validar a empresa");
                }
                if (companyId != user.Company.Id.ToString())
                {
                    return BadRequest("Houve um erro na hora de validar a empresa");
                }
                var resultado = await _confirmService.ConfirmPresenceWorker(idFuncionario, confirm);
                if (resultado.Item1 == 400)
                {
                    return BadRequest(resultado.Item2);
                }
                return Ok(resultado.Item2);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }


        [HttpGet("obter-dias-nao-comparecidos/{idFuncionario}")]
        public async Task<IActionResult> GetNotPresenceDaysConfirmed(string idFuncionario)
        {
            var resultado = await _confirmService.NotPresenceOfConfirmedDay(idFuncionario);
            return Ok(resultado);
        }
    }
}
