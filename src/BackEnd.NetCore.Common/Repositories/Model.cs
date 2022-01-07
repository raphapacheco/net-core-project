using FluentValidation.Results;

namespace BackEnd.NetCore.Common.Repositories
{
    public abstract class Model
    {
        public int Id { get; set; }

        public abstract bool Valido(out ValidationResult resultadoValidacao);

    }
}
