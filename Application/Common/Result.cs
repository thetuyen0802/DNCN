using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Result
    {
        public string message { get; init; }
        public bool succes { get; init; }
        public string? ErrorCode { get; init; }
        public Exception? exeption { get; init; }

        protected Result(string mess,bool succ, string? erc = null , Exception? ex = null) 
        {
            message = mess;
            succes = succ;
            ErrorCode = erc;
            exeption = ex;
        }
        public static Result Ok(string mess = "Done") => new Result(mess, true);
        public static Result Fail(string mess , string? errorCode , Exception? exeption) => new Result(mess, false,errorCode,exeption);
        public static Result<T> Ok<T>(T data, string message = "Done") => new(data, message,true );

        public static Result<T> Fail<T>(string message, string? errorCode = null, Exception? ex = null)
            => new(default, message,false , errorCode, ex);
    }
    public class Result<T> : Result
    {
        public T? Data { get; init; }
        internal Result (T data, string message, bool succes, string? ercode = null ,Exception? exeption = null ) : base(message, succes, ercode, exeption)
            {
                Data = data;
            }
    }
}
