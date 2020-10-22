using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.ContactForm;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    public class ContactController : Controller
    {
        private readonly EmailAddress _fromAddress;
        private readonly IEmailServicee _emailService;

        public ContactController(EmailAddress fromAddress, IEmailServicee emailService)
        {
            _fromAddress = fromAddress;
            _emailService = emailService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                EmailMessage msgToSend = new EmailMessage
                {
                    FromAddresses = new List<EmailAddress> { _fromAddress },
                    ToAddresses = new List<EmailAddress> { _fromAddress },
                    Content = $"Here is your message: Name: {model.Name}, " +
                        $"Email: {model.Email}, Phone: {model.Phone} Message: {model.Message}",
                    Subject = "Сообщение с контактной формы"
                };

                _emailService.Send(msgToSend);
                return RedirectToAction("Index");
            }
            else
            {
                return Index();
            }
        }
    }
}
