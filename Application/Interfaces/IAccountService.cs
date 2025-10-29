using API.DTOs.Request.Login;
using Application.Common;
using Application.DTOs.Requests.Account;
using Application.DTOs.Responses.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<Result> Login(LoginRequest loginRequest);
        Task<Result> RegisterAccount(RegisterAccountRequest request);
    }
}
