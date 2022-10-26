namespace CompanyManagerAPI.Models.Security
{
    public class AuthenticationResponseModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }        
        public string RefreshToken { get; set; }        
    }
}
