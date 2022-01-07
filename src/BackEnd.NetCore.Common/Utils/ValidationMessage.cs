using System.Diagnostics.CodeAnalysis;

namespace BackEnd.NetCore.Common.Utils
{
    [ExcludeFromCodeCoverage]
    public static class ValidationMessage
    {
        private const string ENUM_CONVERTION_FAILED = "Não foi possível converter. Valores aceitos: {0}";
        private const string MAXIMUM_LENGTH = "O tamanho máximo é {0}";

        public const string NOT_EMPTY = "Não pode estar vazio";
        public const string NOT_NULL = "Não pode ser nulo";
        public const string REQUIRED = "Obrigatório";
        public const string INVALID = "Inválido";
                           

        public static string EnumConvertionFailed(string acceptedValues)
        {
            return string.Format(ENUM_CONVERTION_FAILED, acceptedValues);
        }

        public static string MaximumLength(int length)
        {
            return string.Format(MAXIMUM_LENGTH, length);
        }
    }
}
