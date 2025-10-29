using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RoleRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }
        
        public async Task AddRoleAsync(Role role)
        {
            const string Sql = @"
                INSERT INTO Roles (RoleName, IsActive, Description, CreatedAt, UpdatedAt)
                VALUES ( @RoleName, @IsActive, @Description, @CreatedAt, @UpdatedAt)";
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role), "Role cannot be null");
            }
            else
            {
                using (var connect = _connectionFactory.CreateConnection())
                {
                    connect.Open();
                    using (var tran  = connect.BeginTransaction())
                    {
                        try
                        {
                            await connect.ExecuteAsync(Sql, new
                            {

                                RoleName = role.RoleName,
                                IsActive = role.IsActive,
                                Description = role.Description,
                                CreatedAt = DateTimeOffset.Now,
                                UpdatedAt = DateTimeOffset.Now,
                            },transaction:tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw new Exception("Insert to database Fail" , ex);
                        }
                        
                    }
                   
                }
            }
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            const string Sql = "UPDATE Role SET IsActive = 0 WHERE RoleId = @id ";
            await RoleExistsAsync(id);
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(Sql, new { id });
            }
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            const string Sql = "SELECT RoleId, RoleName, IsActive, Description, CreatedAt, UpdatedAt  FROM Roles";
            using (var connection = _connectionFactory.CreateConnection())
            {
               return await connection.QueryAsync<Role>(Sql);
            }
        }

        public async Task<Role> GetRoleByIdAsync(Guid id)
        {
            if ( id == Guid.Empty)
            {
                throw new ArgumentException("Id must be a valid non-empty GUID", nameof(id));
            }

            const string Sql = "SELECT RoleId, RoleName, IsActive, Description, CreatedAt, UpdatedAt FROM Roles WHERE RoleId = @RoleId";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var role = await connection.QuerySingleOrDefaultAsync<Role>(Sql, new { RoleId = id }).ConfigureAwait(false);
                if (role == null)
                {
                     throw new KeyNotFoundException($"Role with ID {id} not found.");
                }
                return role;
            }
        }
         
        public async Task<IEnumerable<Role>> GetRoleByNameAsync(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Id must be a valid non-empty GUID", nameof(name));
            }

            const string Sql = "SELECT RoleId, RoleName, IsActive, Description, CreatedAt, UpdatedAt FROM Roles WHERE RoleName = @RoleName";
            using (var connection = _connectionFactory.CreateConnection())
            {
                var role = await connection.QueryAsync<Role>(Sql, new {RoleName = name}).ConfigureAwait(false);
                if (role == null)
                {
                    throw new KeyNotFoundException($"Role with ID {name} not found.");
                }
                return role;
            }
        }

        public async Task<IEnumerable<Role>> GetRoleIsActive()
        {
            const string Sql = "SELECT RoleId, RoleName, IsActive, Description, CreatedAt, UpdatedAt FROM Roles WHERE IsActive = 1";
            using (var connection = _connectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Role>(Sql);
            }
        }

        public async Task<bool> RoleExistsAsync(Guid id)
        {
            const string Sql = "SELECT COUNT(1) FROM Roles WHERE RoleId = @RoleId";
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id must be a valid non-empty GUID", nameof(id));
            }
            using (var connection = _connectionFactory.CreateConnection())
            {
                 var role = await connection.ExecuteScalarAsync<int>(Sql, new { RoleId = id });
                return role > 0;
            }
        }

        public async Task UpdateRoleAsync(Role role)
        {
            bool exists = await RoleExistsAsync(role.RoleId);
            if (!exists)
            {
                throw new ArgumentException("Role does not exist", nameof(role.RoleId));
            }
            const string Sql = @"
                UPDATE Roles 
                SET RoleName = @RoleName, 
                    IsActive = @IsActive, 
                    Description = @Description, 
                    UpdatedAt = @UpdatedAt 
                WHERE RoleId = @RoleId";
            using(var connect = _connectionFactory.CreateConnection())
            {
                connect.Open();
                using (var tran = connect.BeginTransaction())
                {
                    try
                    {
                        await connect.ExecuteAsync(Sql, new
                        {
                            RoleId = role.RoleId,
                            RoleName = role.RoleName,
                            IsActive = role.IsActive,
                            Description = role.Description,
                            CreatedAt = DateTimeOffset.Now,
                            UpdatedAt = DateTimeOffset.Now,
                        },transaction:tran);
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw new Exception("Update error");
                    }
                }
            }

        }
    }
}
