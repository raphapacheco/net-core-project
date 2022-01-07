using FluentValidation.Results;
using System.Collections.Generic;

namespace BackEnd.NetCore.Common.Utils
{
    public class ErrorMessage
    {
        private List<Error> _erros;
        public string Mensagem { get; }
        public IEnumerable<Error> Erros => _erros;
        
        public ErrorMessage(string menssgem, IEnumerable<ValidationFailure> validationFailures)
        {
            Mensagem = menssgem;
            _erros = new List<Error>();

            foreach (var failure in validationFailures)
            {
                _erros.Add(new Error(failure));
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
