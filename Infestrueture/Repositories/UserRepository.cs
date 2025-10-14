using Dapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Repositories;
using Infrastructure.Data;
using Domain.Enum;
using Infrastructure.Services.PasswordHasher;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Infestrueture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

        }
        //// ID người dùng
        //public Guid UserId { get; set; }

        //// Thông tin đăng nhập
        //public string UserName { get; set; }
        //public string PasswordHash { get; set; }
        //public string Email { get; set; }

        //// Thông tin cá nhân
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string PhoneNumber { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        //public GenderEmun Gender { get; set; } // 'M', 'F', 'O'

        //// Thông tin xác thực
        //public bool EmailVerified { get; set; }
        //public bool PhoneVerified { get; set; }
        //public string VerificationToken { get; set; }
        //public DateTimeOffset? VerificationTokenExpiry { get; set; }
        //public string ResetToken { get; set; }
        //public DateTimeOffset? ResetTokenExpiry { get; set; }

        //// Trạng thái và thời gian
        //public UserStatusEmun Status { get; set; }
        //public DateTimeOffset? LastLoginAt { get; set; }
        //public int LoginAttempts { get; set; }
        //public DateTimeOffset? LockoutUntil { get; set; }

        //// Metadata
        //public DateTimeOffset CreatedAt { get; set; }
        //public DateTimeOffset UpdatedAt { get; set; }
        //public Guid? CreatedBy { get; set; }
        //public Guid? UpdatedBy { get; set; }

        //// Khóa ngoại
        //public Guid RoleId { get; set; }

        //// Thuộc tính tính toán (không cần mapping)
        //public string FullName => $"{FirstName} {LastName}";
        public async Task AddUserAsync(User user)
        {
            const string sql = @"INSERT INTO Users (UserName , PasswordHash , Email , FirstName , LastName , PhoneNumber , DateOfBirth , Gender , EmailVerified , PhoneVerified , VerifcationToken , VerificationTokenExpiry , Status , LastLoginAt , LoginAttempts , LockoutUntil , CreatedAt ,UpdatedAt , CreatedBy , UpdatedBy , RoleId )
              VALUES (@UserName , @PasswordHash , @Email , @FirstName , @LastName , @PhoneNumber , @DateOfBirth , @Gender , @EmailVerified , @PhoneVerified , @VerificationToken , @VerificationTokenExpiry , @Status , @LastLoginAt , @LoginAttempts , @LockoutUntil , @CreatedAt ,@UpdatedAt , @CreatedBy , @UpdatedBy , @RoleId )";
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(sql, new
                        {
                            user.UserName,
                            user.PasswordHash,
                            user.Email,
                            user.FirstName,
                            user.LastName,
                            user.PhoneNumber,
                            user.DateOfBirth,
                            user.Gender,
                            user.EmailVerified,
                            user.PhoneVerified,
                            user.VerificationToken,
                            user.VerificationTokenExpiry,
                            user.Status,
                            user.LastLoginAt,
                            user.LoginAttempts,
                            user.LockoutUntil,
                            user.CreatedAt,
                            user.UpdatedAt,
                            user.CreatedBy,
                            user.UpdatedBy,
                            user.RoleId
                        });
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw new Exception("Insert Error");
                        
                    }
                   
                }
            }

        }

        public async Task DeleteUserAsync(int id)
        {

            const string sql = "UPDATE Users SET Status = 1  WHERE UserId = @UserId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { UserId = id });
                if (result == 0)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }
            }

        }

        public async Task<bool> EmailExists(string email)
        {
            const string sql = "SELECT COUNT(1) FROM Users WHERE Email =@Email";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(sql, new { Email = email });
                return result > 0;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            const string sql = "SELECT * FROM Users ";
            using ( var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<User>(sql);
                return result; 
            };

        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            const string sql = "SELECT * FROM Users WHERE Email =@email ";
            using( var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<User>(sql,email);
                return result;
                //result nullable
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Users WHERE UserId =@id ";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<User>(sql,id);
                return result;
                //result nullable
            }
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(Guid roleId)
        {
            const string sql = "SELECT * FROM Users WHERE RoleId =@roleId ";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<User>(sql,roleId);
                return result;
                //result nullable
            }
        }

        public async Task<IEnumerable<User>> GetUsersByStatusAsync(UserStatusEmun status)
        {
            const string sql = "SELECT * FROM Users WHERE Status =@status ";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<User>(sql, status);
                return result;
                //result nullable
            }
        }

        public async Task<string> GetPasswordByUsername(string username)
        {
            const string sql = "SELECT PasswordHash FROM User WHERE @UserName = username ";
            using(var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<string>(sql, username);
                return result.ToString() ;
            }
        }

        public async Task<bool> PhoneExists(string phoneNumber)
        {
            const string sql = "SELECT COUNT(1) FROM Users WHERE PhoneNumber =@phoneNumber ";
            using(var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(sql, phoneNumber);
                return result > 0;
            }
        }
        
        
        public async Task UpdateUserAsync(User user)
        {
            const string sql = "UPDATE Users SET PasswordHash = @PasswordHash,Email = @Email,    FirstName = @FirstName,    LastName = @LastName,    PhoneNumber = @PhoneNumber,    DateOfBirth = @DateOfBirth,    Gender = @Gender, EmailVerified = @EmailVerified,    PhoneVerified = @PhoneVerified,    VerificationToken = @VerificationToken,   VerificationTokenExpiry = @VerificationTokenExpiry,    ResetToken = @ResetToken,  ResetTokenExpiry = @ResetTokenExpiry,   Status = @Status,    LastLoginAt = @LastLoginAt,    LoginAttempts = @LoginAttempts,   LockoutUntil = @LockoutUntil,    UpdatedAt = @UpdatedAt,    UpdatedBy = @UpdatedBy,    RoleId = @RoleId WHERE UserId = @UserId ";

            using( var connection = _connectionFactory.CreateConnection())
            {
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(sql, new
                        {
                           
                            user.PasswordHash,
                            user.Email,
                            user.FirstName,
                            user.LastName,
                            user.PhoneNumber,
                            user.DateOfBirth,
                            user.Gender,
                            user.EmailVerified,
                            user.PhoneVerified,
                            user.VerificationToken,
                            user.VerificationTokenExpiry,
                            user.Status,
                            user.LastLoginAt,
                            user.LoginAttempts,
                            user.LockoutUntil,
                            user.CreatedAt,
                            user.UpdatedAt,
                            user.CreatedBy,
                            user.UpdatedBy,
                            user.RoleId
                        });

                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw new Exception("Update Error");
                    }
                   

                }
            }
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            const string sql = "SELECT COUNT(1) FROM Users WHERE UserId = @Userid";
            using( var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(sql,id);
                return result > 0;
            }
        }

        public async Task<bool> UsernameExists(string username)
        {
            const string sql = "SELECT COUNT(1) FROM Users WHERE UserName  = @UserName";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(sql, username);
                return result > 0;
            }
        }
        public async Task<User> GetUserByUsername(string username)
        {
            const string sql = "SELECT * FROM Users WHERE UserName  = @UserName";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QuerySingleAsync(sql, username);
                return result;
            }
        }
    }
}
