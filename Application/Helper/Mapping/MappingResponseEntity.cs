using Application.DTOs.Responses.Account;
using Application.DTOs.Responses.Role;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper.Mapping
{
    public class MappingResponseEntity:Profile
    {

        public MappingResponseEntity()
        {   
            #region//Map CreateRoleResponse - Role
            CreateMap<CreateRoleResponse, Role>()
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();
            #endregion
            #region Map registeracccount -user

            CreateMap<RegisterAccountResponse, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.EmailVerified, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneVerified, opt => opt.Ignore())
                .ForMember(dest => dest.VerificationToken, opt => opt.Ignore())
                .ForMember(dest => dest.VerificationTokenExpiry, opt => opt.Ignore())
                .ForMember(dest => dest.ResetToken, opt => opt.Ignore())
                .ForMember(dest => dest.ResetTokenExpiry, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutUntil, opt => opt.Ignore())
                .ForMember(dest => dest.LoginAttempts, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ReverseMap();
            #endregion
        }
    }
}
