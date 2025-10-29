using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Redis
{
    public interface ICacheService
    {
        Task SetAsync (string key, string value, TimeSpan? expiry = null);
        Task GetAsync( string key);
        Task Remove( string key);
    }
}
