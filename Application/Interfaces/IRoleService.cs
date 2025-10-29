using Application.Common;
using Application.DTOs.Requests.Role;
using Application.DTOs.Responses.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<Result<CreateRoleResponse>> CreateRole(CreateRoleRequest role);
    }
}
