using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

namespace UTEMerchant
{
    static class SendMail
    {
        public static void Send(string verifycode, string gmail)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("UTEMerchant", "onluyen122004@gmail.com"));
            email.To.Add(MailboxAddress.Parse($"{gmail}"));
            email.Subject = "Verify your email";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Verify code: " + verifycode
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("onluyen122004@gmail.com", "ignulqpnyfphsgky");
                smtp.Send(email);
                smtp.Disconnect(true);

            }
        }
    }
}
