using Application.DTOs.Requests.Account;
using Application.DTOs.Requests.Role;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Domain.Entities;
using Infrastructure.Services.PasswordHasher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper.Mapping
{
    public class MappingProffile :Profile
    {
        public MappingProffile()
        {
            //Map RegiterAccountRequest với User
            #region
            CreateMap<RegisterAccountRequest, User>()
                // IGNORE - vì không tồn tại hoặc sẽ được tạo riêng
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                // Lý do: UserId chưa có, sẽ được tạo Guid.NewGuid() ở service layer
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                // Lý do: Request có Password (plain text), nhưng User cần PasswordHash (hashed)
                // Không thể map trực tiếp, phải hash riêng ở service layer
                .ForMember(dest => dest.VerificationToken, opt => opt.Ignore())
                // Lý do: Token được tạo khi cần verify email, không phải lúc register
                .ForMember(dest => dest.VerificationTokenExpiry, opt => opt.Ignore())
                // Lý do: Expiry time được set cùng lúc với token
                .ForMember(dest => dest.ResetToken, opt => opt.Ignore())
                // Lý do: Token reset password chỉ được tạo khi user quên password
                .ForMember(dest => dest.ResetTokenExpiry, opt => opt.Ignore())
                // Lý do: Expiry time được set cùng lúc với token
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                // Lý do: UpdatedAt chỉ được set khi có update, lúc register không cần
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                // Lý do: UpdatedBy chỉ được set khi có update
                .ForMember(dest => dest.FullName, opt => opt.Ignore())
                // Lý do: Computed property, được tính từ FirstName + LastName, không cần map
                .ReverseMap()
                .ForMember(dest=> dest.Password, opt=> opt.Ignore()) // K trả password ra client
                .ForMember(dest => dest.FullName, opt => opt.Ignore()); // Reverse cũng ignore FullName
            #endregion
            //Map CreateRoleRequest với Role
            #region 
            CreateMap<CreateRoleRequest, Role>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ReverseMap();

            #endregion
        }

    }


       

    }
