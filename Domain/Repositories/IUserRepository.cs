using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(Guid id);
        Task<IEnumerable<User>> GetUsersByRoleAsync(Guid roleId);
        Task<IEnumerable<User>> GetUsersByStatusAsync(UserStatusEnum status);
        Task <string> GetPasswordByUsername(string username);
        Task<bool> UsernameExists(string username);
        Task<User> GetUserByUsername(string username);
        Task<bool> EmailExists(string email);
        Task<bool> PhoneExists(string phoneNumber);

    }
}
