using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepositories
    {
        private readonly IConnectionFactory _connectionFactory;
        public RoleRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }
        public Task AddRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            var Sql = "SELECT * FROM Roles";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryAsync<Role>(Sql);
            }
        }

        public async Task<Role> GetRoleByIdAsync(Guid id)
        {
            if ( id == Guid.Empty)
            {
                throw new ArgumentException("Id must be a valid non-empty GUID", nameof(id));
            }

            const string Sql = "SELECT * FROM Roles WHERE RoleId = @RoleId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Role>(Sql, new { RoleId = id }).ConfigureAwait(false);
            }
        }
         
        public Task<Role> GetRoleByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetRoleIsActive()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RoleExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
