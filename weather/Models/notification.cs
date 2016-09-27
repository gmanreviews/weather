using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace weather.Models
{
    public class notification
    {
        public string subject { get; set; }
        public string content { get; set; }
        public string email_to { get; set; }
        public string type { get; set; }

        public notification() { }
        public notification(string subject, string content, string email_to, string type)
        {
            this.subject = subject;
            this.content = content;
            this.email_to = email_to;
            this.type = type;
        }
    }
    public class notification_model
    {
        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }

        public static void send_note_to_many(List<person> people, string type)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                actual_email_worker(people, type);
            }).Start();
        }

        private static void actual_email_worker(List<person> people, string type)
        {
            foreach (person p in people)
            {
                send_notification(new notification("Work Day Update", generate_email_content(p, type), p.email, type), type);
            }
        }

        private static void send_notification(notification note, string type)
        {
            try
            {
                //write code to send email
                SmtpClient smtp_client = new SmtpClient();
                MailAddress from = new MailAddress(
                    System.Configuration.ConfigurationManager.AppSettings["smtp_email"].ToString(),
                    System.Configuration.ConfigurationManager.AppSettings["smtp_from"].ToString(),
                    System.Text.Encoding.UTF8);
                MailAddress to = new MailAddress(note.email_to);
                MailMessage msg = new MailMessage(from, to);
                msg.Body = note.content;
                msg.Subject = note.subject;
                smtp_client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                smtp_client.SendAsync(msg, "test");
                // Clean up.
                msg.Dispose();
            }
            catch (Exception e)
            {
                log_message_to_db(note);
            }
        }
         
        private static string generate_email_content(person person, string type)
        {
            string message = "";
            message += "Dear " + person.first_name + " " + person.last_name + ",";
            switch (type)
            {
                case "bad":
                    message += "Due to inclimate weather in the region, " + person.location.name + ", of your employ you will only be required to do a half day's, 4 hour, work. Thank you for your efforts.";
                    break;
                case "good":
                    message += "Due to clear weather in the region, " + person.location.name + ", of your employ your regular work schedule, 8 hour, is not impacted. Thank you for your efforts.";
                    break;
                default:
                    break;
            }
            message += "Sincerely, Management.";
            return message;
        }

        private static void log_message_to_db(notification note)
        {
            note = clean_note(note);
            db db = new db();
            db.connect();
            SqlDataReader reader = db.query_db("EXEC log_message '" + note.email_to +
                                                              "','" + note.subject +
                                                              "','" + note.content + "'");
            db.disconnect();
        }

        private static notification clean_note(notification note)
        {
            note.content = general.clean_string(note.content);
            note.subject = general.clean_string(note.subject);
            note.email_to = general.clean_string(note.email_to);
            return note;
        }
    }
}