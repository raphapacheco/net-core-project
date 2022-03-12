using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.NetCore.Common.DataContracts
{
    public class AutenticacaoRequest
    {
        [Required(ErrorMessage = "Informe os dados do usuário")]
        [BindProperty(Name = "user")]
        public string Usuario { get; set; }        
    }
}
