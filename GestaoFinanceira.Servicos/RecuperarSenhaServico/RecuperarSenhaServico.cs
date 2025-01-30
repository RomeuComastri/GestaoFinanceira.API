using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GestaoFinanceira.Servicos.Interfaces;

namespace GestaoFinanceira.Servicos.RecuperarSenhaServico
{
    public class RecuperarSenhaServico : IRecuperarSenhaServico
    {
        public async Task EnviarEmailRecuperacaoAsync(string email, string codigo)
        {
            try
            {
                using (var smtpClient = new SmtpClient("smtp-relay.brevo.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("80d49d002@smtp-brevo.com", "jvbYRqySIx7Tm1p8");
                    smtpClient.EnableSsl = true;

                    string emailBody = $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Recuperação de Senha</title>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }}
                            .email-container {{
                                max-width: 600px;
                                margin: 20px auto;
                                background: #ffffff;
                                border-radius: 8px;
                                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                padding: 20px;
                                text-align: center;
                            }}
                            .header {{
                                background-color: #00a455;
                                color: white;
                                padding: 15px 0;
                                font-size: 20px;
                                border-radius: 8px;
                            }}
                            .content {{
                                margin: 20px 0;
                                color: #333333;
                                line-height: 1.6;
                            }}
                            .code {{
                                font-size: 24px;
                                font-weight: bold;
                                color: #00a455;
                                margin: 10px 0;
                            }}
                            .footer {{
                                font-size: 12px;
                                color: #888888;
                                margin-top: 20px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='header'>Recuperação de Senha - FinPlanner</div>
                            <div class='content'>
                                <p>Olá,</p>
                                <p>Recebemos uma solicitação para redefinir sua senha. Use o código abaixo para continuar:</p>
                                <div class='code'>{codigo}</div>
                                <p>Se você não solicitou a recuperação, por favor, ignore este e-mail.</p>
                            </div>
                            <div class='footer'>
                                <p>FinPlanner - Sua gestão financeira simplificada.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("romeu.itera360@gmail.com", "FinPlanner"),
                        Subject = "Recuperação de Senha",
                        Body = emailBody,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(email);
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"Erro SMTP: {smtpEx.Message} - {smtpEx.StatusCode}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral ao enviar e-mail: {ex.Message}");
                throw;
            }
        }
    }
}
