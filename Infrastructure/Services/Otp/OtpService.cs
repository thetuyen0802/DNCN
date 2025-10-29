using Infrastructure.Services.OPT;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Otp
{
    public class OtpService : IOtpService
    {
        private readonly IMemoryCache _cache;
        private Random _random = new();

        public OtpService(IMemoryCache cache, Random random)
        {
            _cache = cache;
            _random = random;
        }

        public Task<string> GenerateOtpAsync(string userId)
        {
            string otp = _random.Next(111111, 999999).ToString();
            _cache.Set(userId, otp);
        }
        
        public Task<bool> VerifyOtpAsync(string userId, string otp)
        {
            throw new NotImplementedException();
        }
    }
}
