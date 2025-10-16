namespace Application.DTOs.Responses.Account
{
    public class LoginResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }
}
