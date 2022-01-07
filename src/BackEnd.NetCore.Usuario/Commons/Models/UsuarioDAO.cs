using BackEnd.NetCore.Common.Repositories;
using FluentValidation.Results;
using System;

namespace BackEnd.NetCore.Usuario.Commons.Models
{
    public class UsuarioDAO : Entidade
    {        
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

        public override bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new UsuarioValidador().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}