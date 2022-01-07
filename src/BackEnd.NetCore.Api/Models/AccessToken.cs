namespace BackEnd.NetCore.Api.Models
{
    public class AccessToken
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
