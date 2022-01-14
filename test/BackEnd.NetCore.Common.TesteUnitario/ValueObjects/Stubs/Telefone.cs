using BackEnd.NetCore.Common.ValueObjects;
using System.Collections.Generic;

namespace BackEnd.NetCore.Common.TesteUnitario.ValueObjects.Stubs
{
    public class Telefone : ValueObject
    {
        public Telefone(int ddd, string numero)
        {
            DDD = ddd;
            Numero = numero;
        }

        public int DDD { get; }

        public string Numero { get; }

        public override object GetValue()
        {
            return ToString();
        }

        public override string ToString()
        {
            return $"({DDD}) {Numero}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DDD;
            yield return Numero;
        }
    }
}
