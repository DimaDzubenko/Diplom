using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.ContactForm
{
    public interface IEmailServicee
    {
        void Send(EmailMessage message);
    }
}
