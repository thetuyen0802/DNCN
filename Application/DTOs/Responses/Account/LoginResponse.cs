namespace Application.DTOs.Responses.Account
{
    public class LoginResponse
    {
        public string message { get; set; }
        public string token { get; set; }
        public int code { get; set; }
    }
}
