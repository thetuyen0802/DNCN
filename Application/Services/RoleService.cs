using Application.Common;
using Application.DTOs.Requests.Role;
using Application.DTOs.Responses.Role;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService:IRoleService
    {
        private readonly IRoleRepository _roleRepository;  
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper) 
        { 
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<Result<CreateRoleResponse>> CreateRole(CreateRoleRequest role)
        {
            try
            {
                Role roleadded = _mapper.Map<Role>(role);
                roleadded.IsActive = true;
                roleadded.CreatedAt = DateTime.Now;
                await _roleRepository.AddRoleAsync(roleadded);

                return Result<CreateRoleResponse>.Ok(_mapper.Map<CreateRoleResponse>(roleadded));
            }
            catch (Exception ex)
            {

                return Result.Fail<CreateRoleResponse>(ex.Message,ErrorCode.DB_INSERT_FAILED,ex);
            }
            
        }

    }
}
