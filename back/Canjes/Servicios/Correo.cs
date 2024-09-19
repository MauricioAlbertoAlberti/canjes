using Canjes.Dto;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace Canjes.Servicios
{
    public static class Correo
    {
        private static string _Host = "smtp.gmail.com";
        private static int _Puerto = 587;
        private static string _NombreEnvio = "Canjes";
        private static string _AppKey = "bwalwdsfehguhngl";
        private static string _Correo = "correobotcanje@gmail.com";

        public static bool EnviarCorreo(CorreoDto correodto)
        {
            try {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_NombreEnvio, _Correo));
                email.To.Add(MailboxAddress.Parse(correodto.Destinatario));
                email.Subject = correodto.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correodto.Contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_Host, _Puerto, SecureSocketOptions.StartTls);

                smtp.Authenticate(_Correo, _AppKey);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch {
                return false; }
        }

    }
}
