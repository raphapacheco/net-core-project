using System.Text;

namespace BackEnd.NetCore.Api.Models
{
    public class ConfiguracaoToken
    {
        public string Secret { get; set; }
        public double ExpiresIn { get; set; }

        public static string Secao => "Token";

        public byte[] GetSecretAsByteArray() => Encoding.ASCII.GetBytes(Secret);
    }
}
