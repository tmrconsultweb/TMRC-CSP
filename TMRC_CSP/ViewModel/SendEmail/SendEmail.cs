using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TMRC_CSP.ViewModel
{
    public class SendEmail
    {
        public static bool SendEmailRegister(string EmailTo, string Subject, string Body, string CCs = "", string footer="Default", string MailAddressRegister = "MailAddressRegister", string MailSmtpRegister = "MailSmtpRegister", string MailPasswordRegister = "MailPasswordRegister")
        {
            //Getting value from WebConfig AppSetting key
            string FromEmail = System.Configuration.ConfigurationManager.AppSettings[MailAddressRegister];
            string SmtpRegister = System.Configuration.ConfigurationManager.AppSettings[MailSmtpRegister];
            string Password = System.Configuration.ConfigurationManager.AppSettings[MailPasswordRegister];

            bool b = false;

            string[] CC = null;
            if (!string.IsNullOrEmpty(CCs))
            {
                string[] arr;
                CC = CCs.Split(',');
            }

            b = SendEmail.SendNotification(EmailTo, FromEmail, Subject, Body,footer, SmtpRegister, FromEmail, Password, 1, CC);
            return b;
        }

        static private bool SendNotification(string toAddress, string fromAddress, string subject, string body,string footer, string smtpaddress, string networkemail, string password, int priorty, string[] CC = null)
        {
            if (footer == "Default")
            {
                body += " <br /><small>We made easy + 24/7 Customer Support Free. We offer helpfull software and professional services that help midsize to large employers operate more effectively, in a lot less time and with a lot less work. In addition, we’re uniquely equipped to help employers deal with the challenges of managing an hourly workforce. Who else can say that?</small>";
            }
            try
            {
                MailAddress senderadress = new MailAddress(fromAddress);
                System.Net.Mail.MailMessage theMailMessage = new System.Net.Mail.MailMessage(fromAddress, toAddress);

                theMailMessage.Body = body;
                theMailMessage.Subject = subject;
                SmtpClient theClient = new SmtpClient(smtpaddress);
                theClient.UseDefaultCredentials = true;

                System.Net.NetworkCredential theCredential = new System.Net.NetworkCredential(networkemail.Replace("yahoo.com", ""), password);
                theClient.Credentials = theCredential;
                //HttpContext.Current.Request.IsLocal ?
                theClient.Port =  25;
                theClient.EnableSsl = true;

                
                theMailMessage.IsBodyHtml = true;
                if (priorty == 1)
                    theMailMessage.Priority = MailPriority.High;
                else if (priorty == 2)
                    theMailMessage.Priority = MailPriority.Normal;
                else
                    theMailMessage.Priority = MailPriority.Low;
                theMailMessage.ReplyTo = senderadress;
                theMailMessage.Sender = senderadress;
                if (CC != null && CC.Length > 0)
                {
                    foreach (string CCAddress in CC)
                        theMailMessage.CC.Add(CCAddress);
                }
                            theClient.Send(theMailMessage);
                theMailMessage.Dispose();
                theMailMessage = null;
            }
            catch (Exception ex)
            {
                return false;
                // MessageBox.Show(ex.Message);
            }
            return true;
        }
    }
}