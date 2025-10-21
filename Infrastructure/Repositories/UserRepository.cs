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

namespace Infrastrueture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

        }
       
        public async Task AddUserAsync(User user)
        {
            const string sql = @"INSERT INTO Users (UserName , PasswordHash , Email , FirstName , LastName , PhoneNumber , DateOfBirth , Gender , EmailVerified , PhoneVerified , VerifcationToken , VerificationTokenExpiry , Status , LastLoginAt , LoginAttempts , LockoutUntil , CreatedAt ,UpdatedAt , CreatedBy , UpdatedBy , RoleId )
              VALUES (@UserName , @PasswordHash , @Email , @FirstName , @LastName , @PhoneNumber , @DateOfBirth , @Gender , @EmailVerified , @PhoneVerified , @VerificationToken , @VerificationTokenExpiry , @Status , @LastLoginAt , @LoginAttempts , @LockoutUntil , @CreatedAt ,@UpdatedAt , @CreatedBy , @UpdatedBy , @RoleId )";
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
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
                        },transaction:tran);
                        tran.Commit();
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
                var result = await connection.QueryFirstOrDefaultAsync<User>(sql,new { Email = email });
                return result;
                //result nullable
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Users WHERE UserId =@id ";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<User>(sql,new { UserId = id });
                return result;
                //result nullable
            }
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(Guid roleId)
        {
            const string sql = "SELECT * FROM Users WHERE RoleId =@roleId ";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<User>(sql,new { RoleId = roleId });
                return result;
                //result nullable
            }
        }

        public async Task<IEnumerable<User>> GetUsersByStatusAsync(UserStatusEnum status)
        {
            const string sql = "SELECT * FROM Users WHERE Status =@status ";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<User>(sql,new{
                    Status = status});
                return result;
                //result nullable
            }
        }

        public async Task<string> GetPasswordByUsername(string username)
        {
            const string sql = "SELECT PasswordHash FROM Users WHERE UserName = @username ";
            using(var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<string>(sql,new { UserName = username });
                return result.ToString() ;
            }
        }

        public async Task<bool> PhoneExists(string phoneNumber)
        {
            const string sql = "SELECT COUNT(1) FROM Users WHERE PhoneNumber =@phoneNumber ";
            using(var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(sql,new { PhoneNumber = phoneNumber });
                return result > 0;
            }
        }
        
        
        public async Task UpdateUserAsync(User user)
        {
            const string sql = "UPDATE Users SET PasswordHash = @PasswordHash,Email = @Email,    FirstName = @FirstName,    LastName = @LastName,    PhoneNumber = @PhoneNumber,    DateOfBirth = @DateOfBirth,    Gender = @Gender, EmailVerified = @EmailVerified,    PhoneVerified = @PhoneVerified,    VerificationToken = @VerificationToken,   VerificationTokenExpiry = @VerificationTokenExpiry,    ResetToken = @ResetToken,  ResetTokenExpiry = @ResetTokenExpiry,   Status = @Status,    LastLoginAt = @LastLoginAt,    LoginAttempts = @LoginAttempts,   LockoutUntil = @LockoutUntil,    UpdatedAt = @UpdatedAt,    UpdatedBy = @UpdatedBy,    RoleId = @RoleId WHERE UserId = @UserId ";

            using( var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(sql, new
                        {
                           user.UserId,
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
                        },transaction:tran);
                        tran.Commit();
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
                var result = await connection.ExecuteScalarAsync<int>(sql,new { UserId = id });
                return result > 0;
            }
        }

        public async Task<bool> UsernameExists(string username)
        {
            const string sql = "SELECT COUNT(1) FROM Users WHERE UserName  = @UserName";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(sql,new { UserName = username });
                return result > 0;
            }
        }
        public async Task<User> GetUserByUsername(string username)
        {
            const string sql = "SELECT * FROM Users WHERE UserName  = @UserName";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QuerySingleAsync(sql, new { UserName = username });
                return result;
            }
        }
    }
}
