using Newtonsoft.Json;

namespace BackEnd.NetCore.Common.DataContracts
{
    public class UsuarioAuth
    {     
        [JsonProperty(PropertyName = "username")]
        public string Nome { get; set; }
 
        [JsonProperty(PropertyName = "password")]
        public string Senha { get; set; }
    }
}
