using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    using Domain.Enum;
    using System;

    public class User
    {
        // ID người dùng
        public Guid UserId { get; set; }

        // Thông tin đăng nhập
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        // Thông tin cá nhân
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderEnum Gender { get; set; } // 'M', 'F', 'O'

        // Thông tin xác thực
        public bool EmailVerified { get; set; } 
        public bool PhoneVerified { get; set; }
        public string VerificationToken { get; set; }
        public DateTimeOffset? VerificationTokenExpiry { get; set; }
        public string ResetToken { get; set; }
        public DateTimeOffset? ResetTokenExpiry { get; set; }

        // Trạng thái và thời gian
        public UserStatusEnum Status { get; set; } 
        public DateTimeOffset? LastLoginAt { get; set; }
        public int LoginAttempts { get; set; }
        public DateTimeOffset? LockoutUntil { get; set; }

        // Metadata
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Khóa ngoại
        public Guid RoleId { get; set; }

        // Thuộc tính tính toán (không cần mapping)
        public string FullName => $"{FirstName} {LastName}";

        // Constructor mặc định
        public User() { }

       
    }
}
