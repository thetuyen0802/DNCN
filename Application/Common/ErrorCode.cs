using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public static class ErrorCode
    {
        // Validation
        public const string VALIDATION_ERROR = "VALIDATION_ERROR";
        public const string NOT_FOUND = "NOT_FOUND";
        public const string DUPLICATE_ENTRY = "DUPLICATE_ENTRY";

        // Database
        public const string DB_CONNECTION_FAILED = "DB_CONNECTION_FAILED";
        public const string DB_INSERT_FAILED = "DB_INSERT_FAILED";
        public const string DB_UPDATE_FAILED = "DB_UPDATE_FAILED";
        public const string DB_DELETE_FAILED = "DB_DELETE_FAILED";

        // Auth
        public const string AUTH_FAILED = "AUTH_FAILED";
        public const string UNAUTHORIZED = "UNAUTHORIZED";

        // System
        public const string UNKNOWN_ERROR = "UNKNOWN_ERROR";
    }
}
