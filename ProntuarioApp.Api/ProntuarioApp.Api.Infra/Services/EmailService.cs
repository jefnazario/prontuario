using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using ProntuarioAppAPI.Infra.Notificacoes;

namespace ProntuarioAppAPI.Infra.Services
{
    public class EmailService : Notificavel
    {
        public void Enviar(string para, string titulo, string mensagem)
        {
            Enviar("contato@maisbolao.com.br", para, titulo, mensagem);
        }

        public void Enviar(string de, string para, string titulo, string mensagem)
        {
            try
            {
                var client = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.sendgrid.net",
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("apikey", "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")
                };

                var mm = new MailMessage(de, para, titulo, mensagem)
                {
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                    ReplyToList = { de }
                };

                client.Send(mm);
                client.Dispose();
            }
            catch (Exception ex)
            {
                AdicionarNotificacao("Erro ao enviar e-mail");
            }
        }
    }
}
