using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests.Account
{
    public class RegisterAccountRequest
    {
       
        // Thông tin đăng nhập
        [Required(ErrorMessage ="User name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]

        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
        ErrorMessage = "Password phải có ít nhất 8 ký tự, bao gồm chữ và số")]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        // Thông tin cá nhân
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; } // 'M', 'F', 'O'
        // Thông tin xác thực
        public bool EmailVerified { get; set; }
        public bool PhoneVerified { get; set; }

        // Trạng thái và thời gian
        public UserStatusEnum Status { get; set; }
        public DateTimeOffset? LastLoginAt { get; set; }
        public int LoginAttempts { get; set; }
        public DateTimeOffset? LockoutUntil { get; set; }

        // Metadata
        public DateTimeOffset CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        // Khóa ngoại
        public Guid RoleId { get; set; }

        // Thuộc tính tính toán (không cần mapping)
        public string FullName => $"{FirstName} {LastName}";
    }
}
