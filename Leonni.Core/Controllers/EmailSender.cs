using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Leonni.Core.Controllers
{
    public static class EmailSender
    {
        public static bool SendMail(string toAddress, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@leonni.com");
            message.To.Add(new MailAddress(toAddress));

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            bool success = false;
            try
            {
                client.Send(message);
                success = true;
            }
            catch (SmtpException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                success = false;
            }
            return success;
        }

        public static void RegistrationConfirmationEmail(string email, string activationLink)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.Subject = "Leonni - Confirmation of registration.";

                StringBuilder msg = new StringBuilder();
                msg.Append("<img src='~/Images/leonni-logo.png' alt='Leonni' /><hr>");
                msg.Append("<p>Hello!</p><br>");
                msg.Append("<p>Now click on the link below to confirm your registration.</p><br>");
                msg.Append("<a href='" + activationLink + "'>" + activationLink + "</a>");
                msg.Append("<p>If the link does not work try to copy the link into your browser bar.</p><br>");
                msg.Append("Have fun!<br>Leonni Team<hr>");
                msg.Append("<p>This message is automatically generated. Replies to this message are not monitored or answered.");
                msg.Append("You received this e-mail because a person has given your e-mail to register. If you have received it in error, DO NOT click the register link and simply delete this e-mail. In a short time, the petition will be removed from the system.</p><br>Thank you");

                message.Body = msg.ToString();
                message.To.Add(email);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                //error in sending mail
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public static void DeleteAccountEmail(string username, string email, string deleteConfirmLink)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.Subject = "Leonni - Delete Account.";

                StringBuilder msg = new StringBuilder();
                msg.Append("<img src='' alt='Leonni' /><hr>");
                msg.Append("<p>Hi " + username + "</p><br>");
                msg.Append("<p>This is the process for deleting your account with Leonni.<br>If you've chosen in error, ignore this email and nothing will change.</p>");
                msg.Append("<p>To confirm deletion it is necessary that you click in the link</p><br>");
                msg.Append("<a href='" + deleteConfirmLink + "'>" + deleteConfirmLink + "</a>");
                msg.Append("Good Luck.<br>Leonni Team<hr>");
                msg.Append("<p>This message is automatically generated. Replies to this message are not monitored or answered.");

                message.Body = msg.ToString();
                message.To.Add(email);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                //error in sending mail
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public static void OutstandingPerson(string username, string email)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.Subject = "Leonni - You are an outstanding person.";

                StringBuilder msg = new StringBuilder();
                msg.Append("<img src='' alt='Leonni' /><hr>");
                msg.Append("<p>Hi " + username + "</p><br>");
                msg.Append("<p>We applied an added value to your account.</p>");
                msg.Append("<p>From this moment you are an OUTSTANDING member.</p>");
                msg.Append("<p>Which means that predominas the rest and be more visible in searches.</p>");
                msg.Append("Good Luck.<br>Leonni Team<hr>");
                msg.Append("<p>This message is automatically generated. Replies to this message are not monitored or answered.");

                message.Body = msg.ToString();
                message.To.Add(email);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                //error in sending mail
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public static void NotOutstandingPerson(string username, string email)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.Subject = "Leonni - You are an outstanding person.";

                StringBuilder msg = new StringBuilder();
                msg.Append("<img src='' alt='Leonni' /><hr>");
                msg.Append("<p>Hi " + username + "</p><br>");
                msg.Append("<p>From this moment you are no longer OUTSTANDING member.</p>");
                msg.Append("Good Luck.<br>Leonni Team<hr>");
                msg.Append("<p>This message is automatically generated. Replies to this message are not monitored or answered.");

                message.Body = msg.ToString();
                message.To.Add(email);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                //error in sending mail
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public static void UserDeleted(string username, string email)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.Subject = "Leonni - Cancellation of account.";

                StringBuilder msg = new StringBuilder();
                msg.Append("<img src='' alt='Leonni' /><hr>");
                msg.Append("<p>Hi " + username + "</p><br>");
                msg.Append("<p>Sorry but our team has deleted your account Leonni.</p><br>");
                msg.Append(@"<p>We have checked all published content and do not conform to the requirements that mark our Privacy Policy and Terms of Use<br>" +
"They violate the basic principles that guarantee that any of us enjoy a safe and responsible Leonni." +
"These principles are designed to be a nice place Leonni and coexistence for all our people and businesses." +
"That said, all information described in your account has been deleted from our system." +
"For questions, suggestions or error, leading to Help.</p>");

                message.Body = msg.ToString();
                message.To.Add(email);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                //error in sending mail
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        public static void PrivateMessage()
        {

        }

        public static void RecoverPassword(string username, string email, string password)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.Subject = "Leonni - Recover Password.";

                StringBuilder msg = new StringBuilder();
                msg.Append("<img src='' alt='Leonni' /><hr>");
                msg.Append("<p>Hi " + username + "</p><br>");
                msg.Append("<p>These are your login details</p><br/>");
                msg.Append("<p>Your Email : </p>" + email);
                msg.Append("<p>Password : </p>" + password);
                msg.Append("<br>Leonni Team<hr>");
                msg.Append("<p>This message is automatically generated. Replies to this message are not monitored or answered.");

                message.Body = msg.ToString();
                message.To.Add(email);

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                //error in sending mail
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}
