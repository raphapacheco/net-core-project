using System;

namespace BackEnd.NetCore.Common.DataContracts
{
    public class RefreshTokenInf
    {
        public string Identificador { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string IP { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
