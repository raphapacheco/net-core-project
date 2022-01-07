using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.NetCore.Common.DataContracts
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "Informe o usuário")]
        [BindProperty(Name = "username")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Token")]
        [BindProperty(Name = "token")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Informe o RefreshToken")]
        [BindProperty(Name = "refreshToken")]
        public string RefreshToken { get; set; }
    }
}
