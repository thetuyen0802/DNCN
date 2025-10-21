
using API.DTOs.Request.Login;
using Application.DTOs.Responses.Account;
using Domain.Entities;
using Domain.Enum;
using Domain.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.JwtServices;
using Infrastructure.Services.PasswordHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AccountService(IJwtService jwtService,IUserRepository userRepository,IPasswordHasher passwordHasher)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            //Test
            if(request.Username == "user1" || request.Password == "1234")
            {
                var fakeUser = new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = "tuyen.nguyen",
                    PasswordHash = "hashed_password_123",
                    Email = "tuyen.nguyen@example.com",

                    FirstName = "Nguyễn",
                    LastName = "Tuyên",
                    PhoneNumber = "0987654321",
                    DateOfBirth = new DateTime(1999, 8, 2),
                    Gender = 0, // ví dụ enum: M = Male, F = Female, O = Other

                    EmailVerified = true,
                    PhoneVerified = false,
                    VerificationToken = Guid.NewGuid().ToString(),
                    VerificationTokenExpiry = DateTimeOffset.UtcNow.AddDays(1),
                    ResetToken = null,
                    ResetTokenExpiry = null,

                    Status = UserStatusEnum.Active, // ví dụ enum: Active, Inactive, Locked,...
                    LastLoginAt = DateTimeOffset.UtcNow.AddDays(-1),
                    LoginAttempts = 0,
                    LockoutUntil = null,

                    CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
                    UpdatedAt = DateTimeOffset.UtcNow,
                    CreatedBy = Guid.NewGuid(),
                    UpdatedBy = Guid.NewGuid(),

                    RoleId = Guid.NewGuid()
                };

                return new LoginResponse() { message="Login success",token = _jwtService.GenarateToken(fakeUser),code = 200 };

            }
            else
            {
                return new LoginResponse() { message = "Login fail", token = "null" , code = 400 };
            }
            //if (await _userRepository.UsernameExists(username))
            //{
            //    string passwordHass  = await _userRepository.GetPasswordByUsername(username);

            //    if (_passwordHasher.Verify(password, passwordHass))
            //    {
            //        return "Đăng nhập thành công " +"Token :" + _jwtService.GenarateToken(await _userRepository.GetUserByUsername(username));
            //    }
            //    else
            //    {
            //        return "Tên tài khoản hoặc mật khẩu không chính xác";   
            //    }
            //}
            //else
            //{
            //    return "Tên tài khoản hoặc mật khẩu không chính xác";
            //}
        }

        //public async Task<string> Register ()
        //{

        //}
    }
}
