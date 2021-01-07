using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TourismDXP.Core.Helper
{
    public class EmailModel
    {
        public string FromEmail { get; set; }
        public string From { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public class EmailSender
    {

        /// <summary>
        /// /
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="htmlContent"></param>
        /// <param name="emailCarbonCopyList"></param>
        /// <returns></returns>
        public static async Task SendEmailAsync(string email, string subject, string htmlContent, List<string> emailCarbonCopyList = null)
        {
            try
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("fissioncrm@north49digital.com", "Fission CRM");
                var to = new EmailAddress(email);
                var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    //success
                }
            }
            catch (Exception ex)
            {
                //RollbarLocator.RollbarInstance.Error(ex.Message);
                //throw;
            }
        }

        /// <summary>
        /// send email by smtp
        /// </summary>
        /// <param name="emailModel"></param>
        /// <param name="LeadName"></param>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        public static async Task SendMail(EmailModel emailModel, string LeadName, int ClientId)
        {
            try
            {
                //using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                //{
                //    MailMessage mail = new MailMessage();
                //    mail.From = new MailAddress("crm@ethnicitymatters.com", "Ethnicity_Matters");
                //    mail.Subject = emailModel.Subject;
                //    if (!string.IsNullOrEmpty(emailModel.CcEmail))
                //        mail.CC.Add(emailModel.CcEmail);

                //    mail.Body = emailModel.Body;
                //    mail.IsBodyHtml = true;
                //    mail.To.Add(new MailAddress(emailModel.ToEmail, LeadName));
                //    smtp.Credentials = new NetworkCredential("crm@ethnicitymatters.com", "6nRb&7kYKtaM");
                //    smtp.EnableSsl = true;
                //    try
                //    {
                //        smtp.Send(mail);
                //        return true;
                //    }
                //    catch (Exception ex)
                //    {
                //        return false;
                //    }
                //}

                var fname = emailModel.From == "" ? "Ethnicity Matters" : emailModel.From;
                var apiKey = "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";
                var client = new SendGridClient(apiKey);

                var from = new EmailAddress(emailModel.From, fname);
                var to = new EmailAddress(emailModel.ToEmail, LeadName);
                var plainTextContent = emailModel.Body;
                var htmlContent = emailModel.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, emailModel.Subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    //success
                }
                //var mailData = new SendEmail()
                //{
                //    Body = body,
                //    SendFrom = frm,
                //    SendTo = toMail,
                //    Subject = subject,
                //    ClientId = ClientId
                //};
                //EmailApiController emailApi = new EmailApiController();
                //emailApi.AddSendEmail(mailData);
                //return response.Result;
            }
            catch (Exception)
            {
            }
        }

    }
}
