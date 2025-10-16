using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using API.DTOs.Request.Login;
using Application.DTOs.Responses.Account;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
