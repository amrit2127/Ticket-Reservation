using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NationalPark_WebApp_116.Models;
using NationalPark_WebApp_116.Repository.IRepository;
using Stripe;
using Stripe.Terminal;
using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace NationalPark_WebApp_116.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly StripeSettings _settings;
        private object StripeConfiguration;
        public BookingController(IBookingRepository bookingRepository,IOptions<StripeSettings> stripeSettings)
        {
            _bookingRepository = bookingRepository;
            _settings = stripeSettings.Value;
        }
        public IActionResult Index()
        {
            BookingTicket booking=new BookingTicket();
            return View(booking);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(BookingTicket booking, int ticketId, string stripetoken)
        {
            if (ModelState.IsValid)
            {
                if (booking == null) return NotFound();

                if (booking.Id == 0)
                {
                    booking.NationalParkId = ticketId;
                    booking.Amount = booking.AdultCount * booking.AdultTicketPrice + booking.ChildrenCount * booking.ChildrenCount;


                    #region Stripe
                    if (stripetoken == null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        //payment process
                        var options = new ChargeCreateOptions()
                        {
                            Amount = Convert.ToInt32(booking.Amount),
                            Currency = "usd",
                            Description = "orderid: " + booking.Id,
                            Source = stripetoken
                        };
                        StripeConfiguration = "sk_test_51O8KJTCIRitaXs1U4UZIXetuWrbXsvWN2K8XinCLsSQastmYhJtjoJ7znbr009zhN8GiJcBCZe1OQImjXWkQkoX000lgfgo8FK";

                        //payment
                        var service = new ChargeService();
                        Charge
                            charge = service.Create(options);

                        if (charge.BalanceTransactionId == null)
                        {
                            booking.PaymentStatus = SD.PaymentStatusRejected;
                        }
                        else
                        {
                            booking.TransactionId = charge.BalanceTransactionId;
                        }
                        if (charge.Status.ToLower() == "succeeded")
                        {
                            booking.PaymentStatus = SD.PaymentStatusApproved;
                            booking.BookingStatus = SD.BookingStatusApproved;
                            booking.DateTime = DateTime.Now;
                        }

                        //twilio message
                        var accountSid = "ACf114e87e0628eb69865bb734b933d9de";
                        var authToken = "e2de8ba24e37c5ec5790ca3ef53c4f0f";
                        TwilioClient.Init(accountSid, authToken);

                        var messageOptions = new CreateMessageOptions(
                          new PhoneNumber("+917717649716"));
                        messageOptions.From = new PhoneNumber("+14699957439");
                        messageOptions.Body = "Booking COnfirmation" +
                            "Your booking has been confirmed. Thank You for choosing us";

                        var message = MessageResource.Create(messageOptions);
                        Console.WriteLine(message.Body);


                        //email verification and message of cancel order
                        try
                        {
                            string smtpServer = "smtp-mail.outlook.com";
                            int smtpPort = 587;
                            string smtpUsername = "amrit2702@outlook.com";
                            string smtpPassword = "amrit@2702";

                            //create message on email
                            MailMessage mailMessage = new MailMessage();
                            mailMessage.From = new MailAddress(smtpUsername);
                            mailMessage.To.Add("amritjyotkaur2702@gmail.com");
                            mailMessage.Subject = "Booking Confirmation";

                            mailMessage.Body = "Your booking has been confirmed. Thank You for choosing us";

                            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                            smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                            smtp.EnableSsl = true;

                            smtp.Send(mailMessage);

                            ViewBag.Message = "Email sent successfully!";
                        }

                        catch (Exception ex)
                        {
                            ViewBag.Error = "An error occurred: " + ex.Message;
                        }

                        TempData["Total"] = booking.Amount;

                        return RedirectToAction("BookingConfirmation", booking.Amount);

                        
                    }
                }
                else
                {
                    return View(booking);
                }


                #endregion

            }
            return RedirectToAction("BookingConfirmation");
        }

        public IActionResult BookingConfirmation()
        {
            if (TempData["Total"] != null)
            {
                ViewData["Amount"] = (int)TempData["Total"];
                return View();
            }
            else
            {
                return RedirectToAction("ErrorAction");
            }
            // return View();
        }

    }
}



