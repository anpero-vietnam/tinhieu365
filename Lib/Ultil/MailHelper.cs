using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace Ultil
{
    public class SendMail : System.Web.UI.Page
    {
        public SendMail()
        {
            //from = @"support@tmtshipping.vn";
            //pass = @"P1v2s3L!o@";
            //port = 25;
            EnableSsl = false;
            settupMailConfig();
        }
        String from;
        String pass;
        String host;
        Boolean EnableSsl;
        //google smtp.Port = 587;
        int port;



        public bool sendMailFromConsole(String displayName, string to, string subject, string body, string appPathFile)
        {
            string path = Directory.GetCurrentDirectory() + appPathFile;
            using (StreamReader reader = File.OpenText(path))
            {
                String Htmlbody = reader.ReadToEnd();
                body = Htmlbody.Replace(@"{mailbody}", body);
            }
            MailMessage mail = new MailMessage();
            var froms = new MailAddress(from, displayName);
            mail.From = froms;

            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            //  smtp.Port = 587;
            smtp.Port = 25;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(froms.Address, pass);
            smtp.EnableSsl = false;
            try
            {
                smtp.Send(mail);
                smtp.Dispose();
                return true;
            }
            catch (Exception)
            {
                //Response.Write(ex.ToString());
                smtp.Dispose();
                return false;
            }
        }
        public bool sendMail(String displayName, string to, string cc, string bcc, string subject, string body, string templatePath)
        {
            if (!string.IsNullOrEmpty(templatePath))
            {
                using (StreamReader reader = File.OpenText(Server.MapPath("~" + templatePath)))
                {
                    string Htmlbody = reader.ReadToEnd();
                    body = Htmlbody.Replace(@"{mailbody}", body);
                }
            }
            
            MailMessage mail = new MailMessage();
            var froms = new MailAddress(from, displayName);
            mail.From = froms;

            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            //smtp.Port = 587;
            smtp.Port = port;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(froms.Address, pass);
            smtp.EnableSsl = EnableSsl;
            smtp.Timeout = 60000;
            try
            {
                smtp.Send(mail);
                smtp.Dispose();
                return true;
            }
            catch (Exception)
            {
                //Response.Write(ex.ToString());               
                smtp.Dispose();
                return false;
            }
        }
        public bool sendMail(String displayName, string to, string subject, string body, String templatePath)
        {
            using (StreamReader reader = File.OpenText(Server.MapPath("~" + templatePath)))
            {
                String Htmlbody = reader.ReadToEnd();
                body = Htmlbody.Replace(@"{mailbody}", body);
            }
            MailMessage mail = new MailMessage();
            var froms = new MailAddress(from, displayName);
            mail.From = froms;

            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            //smtp.Port = 587;
            smtp.Port = port;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(froms.Address, pass);
            smtp.EnableSsl = EnableSsl;
            try
            {
                smtp.Send(mail);
                smtp.Dispose();
                return true;
            }
            catch (Exception)
            {
                //Response.Write(ex.ToString());
                smtp.Dispose();
                return false;
            }
        }
        void settupMailConfig()
        {
            string config = System.Configuration.ConfigurationManager.AppSettings["adminMailConfig"];
            string[] allConfig = config.Split(';');
            foreach (string item in allConfig)
            {
                try
                {
                    if (item.ToLower().Contains("address"))
                    {
                        from = item.Split(':')[1].Trim();
                    }
                    else if (item.ToLower().Contains("pass"))
                    {
                        pass = item.Split(':')[1].Trim();
                    }
                    else if (item.ToLower().Contains("port"))
                    {
                        port = Convert.ToInt32(item.Split(':')[1].Trim());
                    }
                    else if (item.ToLower().Contains("host"))
                    {
                        host = item.Split(':')[1].Trim();
                    }
                    else if (item.ToLower().Contains("enablessl"))
                    {
                        EnableSsl = Convert.ToBoolean(item.Split(':')[1].Trim());
                    }

                }
                catch (Exception)
                {
                }

            }
        }

    }
    public class SendMailConsole
    {
        public SendMailConsole()
        {
            //from = @"support@tmtshipping.vn";
            //pass = @"P1v2s3L!o@";
            //port = 25;
            EnableSsl = false;
            settupMailConfig();
        }
        String from;
        String pass;
        String host;
        Boolean EnableSsl;
        //google smtp.Port = 587;
        int port;
        private string ServiceGetCurrentAppPath()
        {
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            return Path.GetDirectoryName(location);
        }
        public bool SendMail(String displayName, string to, string subject, string body, string appPathFile, bool isSendInService=false)
        {
            string path = Directory.GetCurrentDirectory() + appPathFile;
            if (isSendInService)
            {
                path = ServiceGetCurrentAppPath() + appPathFile;
            }
            using (StreamReader reader = File.OpenText(path))
            {
                String Htmlbody = reader.ReadToEnd();
                body = Htmlbody.Replace(@"{mailbody}", body);
            }
            MailMessage mail = new MailMessage();
            var froms = new MailAddress(from, displayName);
            mail.From = froms;
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            //  smtp.Port = 587;
            smtp.Port = 25;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(froms.Address, pass);
            smtp.EnableSsl = false;
            try
            {
                smtp.Send(mail);
                smtp.Dispose();
                return true;
            }
            catch (Exception)
            {
                //Response.Write(ex.ToString());
                smtp.Dispose();
                return false;
            }
        }
        void settupMailConfig()
        {
            string config = System.Configuration.ConfigurationManager.AppSettings["adminMailConfig"];
            string[] allConfig = config.Split(';');
            foreach (string item in allConfig)
            {
                try
                {
                    if (item.ToLower().Contains("address"))
                    {
                        from = item.Split(':')[1].Trim();
                    }
                    else if (item.ToLower().Contains("pass"))
                    {
                        pass = item.Split(':')[1].Trim();
                    }
                    else if (item.ToLower().Contains("port"))
                    {
                        port = Convert.ToInt32(item.Split(':')[1].Trim());
                    }
                    else if (item.ToLower().Contains("host"))
                    {
                        host = item.Split(':')[1].Trim();
                    }
                    else if (item.ToLower().Contains("enablessl"))
                    {
                        EnableSsl = Convert.ToBoolean(item.Split(':')[1].Trim());
                    }

                }
                catch (Exception)
                {
                }

            }
        }

    }
}
