using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.NetCore.Common.DataContracts
{
    public class AutenticacaoRequest
    {
        [Required(ErrorMessage = "Informe o usuário")]
        [BindProperty(Name = "username")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [BindProperty(Name = "password")]
        public string Senha { get; set; }
    }
}
