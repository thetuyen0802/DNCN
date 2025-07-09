using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRoleRepositories
    {
        // Define methods for role repository
        Task<Role> GetRoleByIdAsync(Guid id);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task AddRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int id);
        Task<Role> GetRoleByNameAsync(string name);
        Task<Role> GetRoleIsActive();
        Task<bool> RoleExistsAsync(Guid id);
    }
}
