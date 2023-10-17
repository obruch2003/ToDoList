using NuGet.Protocol.Plugins;
using System.Net;
using System.Net.Mail;
namespace ToDoList
{
    public class CodeeloMail
    {
        public static MailMessage CreateMail(string emailTo,string subject,string body) 
        {
            var from = new MailAddress("test_obruch@mail.ru", "ToDoList");
            var to = new MailAddress(emailTo);
            var mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            return mail;
        }
        public static void SendMail(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("test_obruch@mail.ru", "CHf73kjf559kmbH9gkHt");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
