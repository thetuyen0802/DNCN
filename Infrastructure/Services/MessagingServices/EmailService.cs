using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.MessagingServices
{
    public class EmailService:IMessagingService
    {
       

        public Task SendAsync(string to, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
