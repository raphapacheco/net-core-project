using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace BackEnd.NetCore.Common.Utils
{
    public class ErrorMessage
    {
        private List<Error> _erros;
        public string Mensagem { get; }
        public IEnumerable<Error> Erros => _erros;
        
        public ErrorMessage(ValidationException exception)
        {
            Mensagem = exception.Message;
            _erros = new List<Error>();

            foreach (var error in exception.Errors)
            {
                _erros.Add(new Error(error));
            }
        }        
    }

    public class Error
    {
        public string Campo { get; set; }
        public string Menssagem { get; set; }

        public Error(ValidationFailure falha)
        {
            Campo = falha.PropertyName;
            Menssagem = falha.ErrorMessage;
        }
    }
}
