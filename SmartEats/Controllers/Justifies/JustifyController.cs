using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Justifies;
using SmartEats.Services.Justifies;

namespace SmartEats.Controllers.Justifies
{
    [ApiController]
    [Route("justificativas")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class JustifyController : StandardController
    {
        public readonly JustifyService _justifyService;

        public JustifyController(JustifyService justifyService)
        {
            _justifyService = justifyService;
        }

        [HttpPost]
        public async Task<IActionResult> NewJustify([FromBody] CreateJustifyDTO create)
        {
            try
            {
                var resultado = await _justifyService.NewJustify(create);
                if (resultado)
                {
                    return Ok("Justificativa Enviada!");
                }
                return BadRequest("Erro ao cadastrar a sua requisição");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Um erro ocorreu");
            }
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Empresa,RH")]
        public async Task<IActionResult> ListJustifies()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(c => c.Type == "id")?.Value;
                var empresaId = HttpContext.User.FindFirst(c => c.Type == "companyId")?.Value;
                if (userId == null)
                {
                    return BadRequest("Token Invalido");
                }
                if (empresaId == null)
                {
                    return BadRequest("Erro tente mais tarde");
                }
                var resultado = await _justifyService.ListJustifies(int.Parse(empresaId), userId);
                if (resultado != null)
                {
                    return Ok(resultado);
                }                
                return BadRequest("Erro ao fazer a sua requisição");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Um erro ocorreu");
            }
        }

        [HttpPut]
        public async Task<IActionResult> ConfirmJustify([FromBody] ConfirmJustifyDTO confirmJustifyDto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(c => c.Type == "id")?.Value;
                var empresaId = HttpContext.User.FindFirst(c => c.Type == "companyId")?.Value;
                if (userId == null || userId != confirmJustifyDto.IdAprovador)
                {
                    return BadRequest("Token Invalido");
                }
                if (empresaId == null || empresaId != confirmJustifyDto.IdEmpresa.ToString())
                {
                    return BadRequest("Erro tente mais tarde");
                }
                var resultado = await _justifyService.ConfirmJustify(confirmJustifyDto);
                if (confirmJustifyDto.Aprovado == true)
                {
                    return Ok("Justificativa Aceita com sucesso!");
                }
                else
                {
                    return Ok("Justificativa Rejeitada com sucesso!");
                }
            }
            catch (Exception ex) {
                return StatusCode(500, "Erro interno ao confirmar uma justificativa");
            }

            
        }
    }
}
