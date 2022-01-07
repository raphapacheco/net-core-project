using System;

namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    public class ConsultarUsuarioQueryResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Celular { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool? Ativo { get; set; } = true;
        public bool? Bloqueado { get; set; } = false;
    }
}
