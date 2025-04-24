using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGenericHost.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string body)
        {
            Console.WriteLine($"Send: {to} - Subject: {subject} - Content: {body}");
        }
    }
}
