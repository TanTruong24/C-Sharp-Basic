using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGenericHost.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string body);
    }
}
