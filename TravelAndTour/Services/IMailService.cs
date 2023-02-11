using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAndTour.Model;

namespace TravelAndTour.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(TravelInformation mailRequest);
    }
}
