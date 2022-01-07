using System.Diagnostics.CodeAnalysis;

namespace BackEnd.NetCore.Common.Utils
{
    [ExcludeFromCodeCoverage]
    public static class ValidationMessage
    {
        private const string NOT_EMPTY = "O campo não pode estar vazio: {0}.";
        private const string NOT_NULL = "O campo não pode ser nulo: {0}.";
        private const string REQUIRED = "Campo obrigatório: {0}.";
        private const string INVALID = "Campo inválido: {0}.";
        private const string ENUM_CONVERTION_FAILED = "Não foi possível converter o campo {0}. Valores aceitos: {1}.";       
        private const string MAXIMUM_LENGTH = "O tamanho máximo para o campo {0} é {1}.";                    

        public static string NotEmpty(string field)
        {
            return string.Format(NOT_EMPTY, field);
        }

        public static string NotNull(string field)
        {
            return string.Format(NOT_NULL, field);
        }
        
        public static string Required(string field)
        {
            return string.Format(REQUIRED, field);
        }
        
        public static string Invalid(string field)
        {
            return string.Format(INVALID, field);
        }

        public static string EnumConvertionFailed(string field, string acceptedValues)
        {
            return string.Format(ENUM_CONVERTION_FAILED, field, acceptedValues);
        }

        public static string MaximumLength(string field, int length)
        {
            return string.Format(MAXIMUM_LENGTH, field, length);
        }
    }
}
