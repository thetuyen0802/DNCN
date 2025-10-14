
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
        public async Task<string> Login(string username , string password)
        {
            if (await _userRepository.UsernameExists(username))
            {
                string passwordHass  = await _userRepository.GetPasswordByUsername(username);

                if (_passwordHasher.Verify(password, passwordHass))
                {
                    return "Đăng nhập thành công " +"Token :" + _jwtService.GenarateToken(await _userRepository.GetUserByUsername(username));
                }
                else
                {
                    return "Tên tài khoản hoặc mật khẩu không chính xác";   
                }
            }
            else
            {
                return "Tên tài khoản hoặc mật khẩu không chính xác";
            }
        }

        //public async Task<string> Register ()
        //{

        //}
    }
}
