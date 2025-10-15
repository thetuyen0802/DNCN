using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.PasswordHasher
{
    public class BcryptOption
    {
        private int _workFactor = 12;
        /// <summary>
        /// Số vòng lặp (2^WorkFactor)
        /// </summary>
        public int WorkFactor
        {
            get => _workFactor;
            set
            {
                if (value < 4 || value > 31)
                    throw new ArgumentOutOfRangeException(nameof(WorkFactor), "Work factor must be between 4 and 31.");
                _workFactor = value;
            }

        }
        /// <summary>
        /// Có kích hoạt entropy nâng cao không
        /// (Tuỳ chọn mở rộng cho tương lai)
        /// </summary>
        
        // Thuộc tính thay đổi độ phức tạp của hàm băm

    }
}
