using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<int> AddUserAsync(User user)
        {
            
            ///SCOPE_IDENTITY() : là một câu lệnh sql lấy ra giá trị IDENTITY gần nhất đc tạo của bảng.
            ///CAST( expression  as data_type)
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                    INSERT INTO Users (Username, Email , PasswordHash , CreatedAt, UpdatedAt , IsActive , StatusId , RoleId ) 
                    VALUES (@Username, @Email , @PasswordHash , @CreatedAt, @UpdatedAt , @IsActive , @StatusId , @RoleId ); 
                    SELECT CAST( SCOPE_IDENTITY() as int);";
            return await connection.QuerySingleAsync<int>(sql,user);
            //Câu lệnh sql thực hiện truy vấn chỉ trả ra một dòng duy nhất và có kiểu dữ liệu xác định :
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        
        Task<IEnumerable<User>> IUserRepository.GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
