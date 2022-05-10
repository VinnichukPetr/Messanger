using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerLibrary.Email
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string File { get; set; }

        public EmailMessage(IEnumerable<string> to, string subject, string content, string file = null)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to as List<MailboxAddress>);
            Subject = subject;
            Content = content;
            File = file;
        }
    }
}
