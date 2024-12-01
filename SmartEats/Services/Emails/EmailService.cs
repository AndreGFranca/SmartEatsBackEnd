using System.Net.Mail;
using System.Net;

namespace SmartEats.Services.Emails
{
    public class EmailService
    {
        IConfiguration configuration = new ConfigurationBuilder()
                                              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                              .AddJsonFile("appsettings.json")
                                              .Build();
        public void SendCodeByEmail(string emailAddress, string code)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(configuration["EmailInfo:email"], configuration["EmailInfo:senha"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("smarteats.org@gmail.com"),
                Subject = "Código de redefinição de senha",
                Body = $"Seu código de redefinição de senha é: {code}",
                IsBodyHtml = true,
            };

            mailMessage.To.Add(emailAddress);

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
            }
        }

        public string GenerateCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper(); // Código de 6 caracteres
        }
    }
}
