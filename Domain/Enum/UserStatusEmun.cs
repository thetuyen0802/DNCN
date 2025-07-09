using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Enum
{
    public enum UserStatusEmun
    {
        Pending = 0, // chờ xử lý
        Active = 1,
        Locked = 2,
        Banned = 3,
        Deleted = 4

    }
}
