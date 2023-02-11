using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using TravelAndTour.Model;
using TravelAndTour.Services;

namespace TravelAndTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMailService _mailService;

        public ServiceController(IMailService mailService)
        {
            this._mailService = mailService;
        }
        [HttpGet]
        public ActionResult CheckApi()
        {
            var message = "i am working";

            return Ok(message);
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(TravelInformation travelInformation)
        {
            try
            {
                await _mailService.SendEmailAsync(travelInformation);
                return Ok("Mail Sended");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        }
}
