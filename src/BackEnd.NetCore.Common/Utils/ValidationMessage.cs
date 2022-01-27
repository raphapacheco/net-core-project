using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace BackEnd.NetCore.Common.Utils
{
    public class ValidationMessage : Exception
    {
        private List<Validation> _erros;
        public IEnumerable<Validation> Erros => _erros;
        public MensagemFormatada MensagemFormatada => new MensagemFormatada(Message, Erros);

        public ValidationMessage(string message, IList<ValidationFailure> failures) : base(message)
        {                        
            _erros = new List<Validation>();

            foreach (var failure in failures)
            {
                _erros.Add(new Validation(failure));
            }
        }        
    }

    public class Validation
    {
        public string Campo { get; set; }
        public string Menssagem { get; set; }

        public Validation(ValidationFailure failure)
        {
            Campo = failure.PropertyName;
            Menssagem = failure.ErrorMessage;
        }
    }

    public class MensagemFormatada
    {
        public IEnumerable<Validation> Erros { get; }
        public string Mensagem { get; }

        public MensagemFormatada(string mensagem, IEnumerable<Validation> erros)
        {
            Mensagem = mensagem;
            Erros = erros;
        }                 
    }
}
