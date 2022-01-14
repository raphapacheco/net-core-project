using BackEnd.NetCore.Common.Models;
using BackEnd.NetCore.Common.ValueObjects;
using System.Collections.Generic;

namespace BackEnd.NetCore.Common.TesteUnitario.ValueObjects.Stubs
{
    public class Codigo : ValueObject
    {
        public Codigo(string codigo)
        {
            Valor = codigo;
        }

        public string Valor { get; }

        public override object GetValue()
        {
            return Valor;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Valor;
        }
    }
}
