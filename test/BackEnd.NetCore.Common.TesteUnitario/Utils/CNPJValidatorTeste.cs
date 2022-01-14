using BackEnd.NetCore.Common.Utils;
using FluentAssertions;
using Xunit;

namespace BackEnd.NetCore.Common.TesteUnitario.Utils
{
    [Trait("Category", "TesteUnitario")]
    [Trait("Class", nameof(CNPJValidator))]
    public class CNPJValidatorTeste
    {
        [Theory(DisplayName = @"Dado um CNPJ valido, quando validado, então deverá resultar verdadeiro")]
        [InlineData("00.000.000/0000-00")]
        [InlineData("92.115.984/0001-74")]
        public void Dado_Um_CNPJ_Valido_Quando_Validado_Entao_Devera_Resultar_Verdadeiro(string cnpj)
        {
            CNPJValidator.Valido(cnpj).Should().BeTrue();
        }

        [Theory(DisplayName = @"Dado um CNPJ invalido, quando validado, então deverá resultar falso")]
        [InlineData("00.001.000/0000-01")]
        [InlineData("00.000.000/000-00")]
        [InlineData("92.515.684/1001-74")]
        public void Dado_Um_CNPJ_Invalido_Quando_Validado_Entao_Devera_Resultar_falso(string cnpj)
        {
            CNPJValidator.Valido(cnpj).Should().BeFalse();
        }        
    }
}
