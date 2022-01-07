using System.Text;

namespace BackEnd.NetCore.Api.Models
{
    public class ConfiguracaoToken
    {
        public double ExpiresIn { get; set; }

        public static string Secao => "Token";
    }
}
