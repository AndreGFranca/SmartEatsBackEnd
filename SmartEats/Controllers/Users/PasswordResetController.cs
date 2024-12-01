using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SmartEats.DTOs.Users;
using SmartEats.Services.Emails;
using SmartEats.Services.Users;

namespace SmartEats.Controllers.Users
{
    [ApiController]
    [Route("senha")]
    public class PasswordResetController : StandardController
    {
        private readonly EmailService _emailService;
        private readonly PasswordResetService _passwordResetService;
        private UserService _userService;

        public PasswordResetController(EmailService emailService, PasswordResetService passwordResetService, UserService cadastroService)
        {
            _emailService = emailService;
            _passwordResetService = passwordResetService;
            _userService = cadastroService;
        }

        // 1. Enviar o código para o e-mail
        [HttpPost("enviar-codigo")]
        public async Task<IActionResult> SendCode([FromBody] SendCodeRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("E-mail é obrigatório.");
            }

            try
            {
                var newCode = new CreatePasswordCodeDTO { 
                    Email = request.Email,
                    Code = _emailService.GenerateCode(),
                    Expiration = DateTime.Now.AddMinutes(15)
                };
                await _passwordResetService.AddCode(newCode);
                _emailService.SendCodeByEmail(request.Email, newCode.Code);

                return Ok("Código enviado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // 2. Validar o código enviado
        [HttpPost("validar-codigo")]
        public async Task<IActionResult> ValidateCode([FromBody] ValidateCodeRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Code))
            {
                return BadRequest("E-mail e código são obrigatórios.");
            }

            try
            {
                var isValid = await _passwordResetService.ValidateCode(request.Email, request.Code);

                if (isValid)
                {
                    return Ok("Código válido.");
                }

                return BadRequest("Código inválido ou expirado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // 3. Atualizar a senha
        [HttpPost("atualizar")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest("As senhas não coincidem.");
            }
            var isValid = await _passwordResetService.ValidateCode(request.Email, request.Code);

            if (!isValid)
            {
                return BadRequest("Código inválido ou expirado.");
            }

            try
            {
                var usuario = await _userService.GetUserByEmail(request.Email);
                var updated = await _userService.ChangePasswordByCode(usuario.Id, request);

                if (updated)
                {
                    return Ok("Senha atualizada com sucesso.");
                }

                return BadRequest("Erro ao atualizar a senha.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
    // Modelos de requisição
    public class SendCodeRequest
    {
        public string Email { get; set; }
    }

    public class ValidateCodeRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
