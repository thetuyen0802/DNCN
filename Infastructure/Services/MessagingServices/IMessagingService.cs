using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.MessagingServices
{
    public interface IMessagingService
    {
        Task SendAsync(string to, string subject, string body);
    }
}
