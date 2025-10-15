using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.JwtServices
{
    public interface IJwtService
    {
        string GenarateToken(User user);
        string RefreshToken();
        ClaimsPrincipal? ValidateToken(string token);

    }
}
