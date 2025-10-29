
using API.DTOs.Request.Login;
using Application.Common;
using Application.DTOs.Requests.Account;
using Application.DTOs.Responses.Account;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.JwtServices;
using Infrastructure.Services.PasswordHasher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService :IAccountService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private IMapper _mapper;

        public AccountService(IJwtService jwtService,IUserRepository userRepository,IPasswordHasher passwordHasher,IMapper mapper)
        {
            _mapper = mapper;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result> Login(LoginRequest request)
        {
            if (await _userRepository.UsernameExists(request.Username))
            {
                string passwordHass = await _userRepository.GetPasswordByUsername(request.Username);

                if (_passwordHasher.Verify(request.Password, passwordHass))
                {

                    return Result<LoginResponse>.Ok<LoginResponse>
                        (
                        new LoginResponse() { code =1 ,
                            message ="Login succes ",
                            token =_jwtService.GenarateToken(await _userRepository.GetUserByUsername(request.Username))}

                        );
                }
                else
                {
                    return Result<LoginResponse>.Fail<LoginResponse>("Login Fail", ErrorCode.AUTH_FAILED, new ArgumentException("The password isn't correct"));
                        
                }
            }
            else
            {
                return Result<LoginResponse>.Fail<LoginResponse>("Login Fail", ErrorCode.AUTH_FAILED, new ArgumentException("Username does not exists"));
            }
        }

        public async Task<Result> RegisterAccount(RegisterAccountRequest request)
        {
            try
            {       //        // Thông tin đăng nhập
                    //        [Required(ErrorMessage = "User name is required")]
                    //        public string UserName { get; set; }
                    //        [Required(ErrorMessage = "Password is required")]

                //        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
                //        ErrorMessage = "Password phải có ít nhất 8 ký tự, bao gồm chữ và số")]
                //        public string Password { get; set; }
                //        [EmailAddress]
                //        public string? Email { get; set; }

                //// Thông tin cá nhân
                //public string FirstName { get; set; }
                //public string LastName { get; set; }
                //[Phone]
                //public string? PhoneNumber { get; set; }
                //public DateTime? DateOfBirth { get; set; }
                //public GenderEnum Gender { get; set; } // 'M', 'F', 'O'
                //// Thông tin xác thực
                //public bool EmailVerified { get; set; }
                //public bool PhoneVerified { get; set; }

                //// Thuộc tính tính toán (không cần mapping)
                //public string FullName => $"{FirstName} {LastName}";

                User accountRegited = _mapper.Map<User>(request);
                accountRegited.CreatedAt = DateTime.UtcNow;
                accountRegited.LoginAttempts = 5;
                accountRegited.PasswordHash = _passwordHasher.Hash(request.Password);
                bool hasEmail = !string.IsNullOrWhiteSpace(request.Email);
                bool hasPhone = !string.IsNullOrWhiteSpace(request.PhoneNumber);
                if (hasEmail && !hasPhone)
                {
                    return Result.Fail("You must enter an email or phone number", null, null);
                }
                accountRegited.PhoneVerified = hasPhone;
                accountRegited.EmailVerified = hasEmail;

                
                await _userRepository.AddUserAsync(accountRegited);
                return Result<RegisterAccountResponse>.Ok<RegisterAccountResponse>(_mapper.Map<RegisterAccountResponse>(accountRegited));
            }
            catch (Exception ex)
            {

                return Result<RegisterAccountResponse>.Fail<RegisterAccountResponse>("RegisterAccount Service Error",ErrorCode.DB_INSERT_FAILED, ex);
            }
            
        }

        
    }
}
