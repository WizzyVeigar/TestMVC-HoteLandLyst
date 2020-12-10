using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMVC_HoteLandLyst.Extensions;
using TestMVC_HoteLandLyst.Models;
using System.Diagnostics;
using TestMVC_HoteLandLyst.DalClasses;
using TestMVC_HoteLandLyst.Interfaces;
using TestMVC_HoteLandLyst.Factories;
using System.Text.Json;
using TestMVC_HoteLandLyst.EmailConfirmation;
using System.Net.Mail;

namespace TestMVC_HoteLandLyst.Controllers
{
    public class CheckOutController : Controller
    {
        FullReservationModel fullReservation;

        private ISqlServerAccess reservationAccess { get; set; }
        private ICustomerAccess customerAccess { get; set; }

        public CheckOutController(ISqlServerAccess dataAccess, ICustomerAccess customerAccess)
        {
            this.reservationAccess = dataAccess;
            this.customerAccess = customerAccess;
        }

        public IActionResult Index()
        {
            try
            {
                //Make in factory
                fullReservation = FullReservationFactory.Instance.CreateSingle(GetReservations());
                return View(fullReservation);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IActionResult MakeReservation(Customer customerValues)
        {
            fullReservation = FullReservationFactory.Instance.CreateSingle(GetReservations());
            if (fullReservation.RoomsToBook == null)
            {
                throw new ArgumentNullException();
            }
            fullReservation.Customer = customerValues;
            if (!((CustomerAccess)customerAccess).FindCustomer(fullReservation.Customer.PhoneNumber))
            {
                ((CustomerAccess)customerAccess).CreateCustomer(fullReservation.Customer);
            }
            ((ReservationAccess)reservationAccess).CreateReservation(fullReservation);

            SendConfirmationEmail(customerValues.Email);

            return RedirectToAction("Index", "Rooms");
        }

        private List<BookingModel> GetReservations()
        {
            return BookingModelFactory.Instance.GetSessionReservations(HttpContext.Session);
        }

        /// <summary>
        /// Send conformation email to customer <paramref name="email"/>
        /// </summary>
        /// <param name="email"></param>
        private void SendConfirmationEmail(string email)
        {
            EmailFactory factory = new EmailFactory();
            MailAddress to = factory.GetMailAddress(email);
            MailAddress from = factory.GetMailAddress("fromAddress");
            SmtpClient client = factory.GetSmtpClient();

            MailMessage message = factory.GetMailMessage(to, from);
            message.Body = "Thank oyu for booking at us";
            message.Subject = "Thanks for your purchase!";

            EmailSender sender = factory.GetEmailSender(message, client, message.To.First(), message.From);
            sender.SendEmail();
        }
    }
}
