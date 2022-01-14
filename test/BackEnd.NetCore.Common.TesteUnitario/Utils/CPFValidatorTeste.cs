using BackEnd.NetCore.Common.Utils;
using FluentAssertions;
using Xunit;

namespace BackEnd.NetCore.Common.TesteUnitario.Utils
{
    [Trait("Category", "TesteUnitario")]
    [Trait("Class", nameof(CPFValidator))]
    public class CPFValidatorTeste
    {
        [Fact(DisplayName = @"Dado um CPF valido, quando validado, então deverá resultar verdadeiro")]
        public void Dado_Um_CPF_Valido_Quando_Validado_Entao_Devera_Resultar_Verdadeiro()
        {
            CPFValidator.Valido("000.000.000-00").Should().BeTrue();
        }

        [Theory(DisplayName = @"Dado um CPF invalido, quando validado, então deverá resultar falso")]
        [InlineData("000.000.000-0")]
        [InlineData("000.000.000-01")]
        public void Dado_Um_CPF_Invalido_Quando_Validado_Entao_Devera_Resultar_falso(string cpf)
        {
            CPFValidator.Valido(cpf).Should().BeFalse();
        }
    }
}
