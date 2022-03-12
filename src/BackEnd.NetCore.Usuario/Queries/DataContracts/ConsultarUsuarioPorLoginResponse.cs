namespace BackEnd.NetCore.Usuario.Queries.DataContracts
{
    public class ConsultarUsuarioPorLoginResponse : ConsultarUsuarioResponse
    {
        public string Senha { get; set; }
    }
}
