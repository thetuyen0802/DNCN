using Application.DTOs.Requests.Role;
using Application.DTOs.Responses.Role;
using AutoMapper;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;  
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper) 
        { 
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<CreateRoleResponse> CreateRole(CreateRoleRequest role)
        {
            await _roleRepository.AddRoleAsync();
        }

    }
}
