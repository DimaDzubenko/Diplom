using Diplom.Helpers;
using Diplom.Services.Models.ShopCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Diplom.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }

        public static Task SendEmailOrderConfirmationAsync(this IEmailSender emailSender, string email, OrderDTO order)
        {
            string text = $"<h1>Dear {order.CustomerName}!</h1><h2>Your order is proceded</h2>" +
                "<h4>Order information:</h4>" +
                $"<p><strong>Created on: </strong>{DateTimeOffset.Now}</p>" +
                $"<p><strong>and will be delivered to: </strong>{order.AddressLine} in two working days</p>" +
                $"<p><strong>Total value: </strong>{order.TotalValue.ToString()} $</p><br/><h3>Contact us on</h3>" +
                $"<p>ofice 5<br>street<br> Kyiv dist<br> <br><strong>Ukraine</strong></p><br>" +
                $"<p>This number is toll free if calling from Ukraine otherwise we advise you to use the electronic form of communication.</p><p>" +
                $"<strong>+38 (000)0000000</strong></p><h3>Electronic support</h3><p>Please feel free to write an email to us.</p>" +
                $"<ul><li><strong><a> nics.company.email@gmail.com</a></strong></li><li><strong><a> nics.company.email@gmail.com</a></strong></li></ul>" +
                $"<h4>Best regards, your Diplom</h4>";
            return emailSender.SendEmailAsync(email, "Your Diplom order", text);
        }
    }
}
