﻿using BackEnd.NetCore.Common.DataContracts;
using FluentValidation.Results;
using MediatR;

namespace BackEnd.NetCore.Usuario.Commands.DataContracts
{    
    public class AtualizarUsuarioCommand : IRequest<ResponseBase>
    {
        public int Id{ get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Celular { get; set; }
        public bool Ativo { get; set; } 

        public bool Valido(out ValidationResult resultadoValidacao)
        {
            resultadoValidacao = new AtualizarUsuarioCommandValidator().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }
}
