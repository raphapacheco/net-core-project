using FluentAssertions;
using BackEnd.NetCore.Common.ValueObjects;
using Xunit;

namespace BackEnd.NetCore.Common.TesteUnitario.ValueObjects
{
    [Trait("Category", "TesteUnitario")]
    [Trait("Class", nameof(Pagination))]
    public class PaginationTeste
    {
        [Theory(DisplayName = @"Dado dois Paginations, quando possuirem o mesmo valor e forem comparadas, então deverá resultar verdadeiro")]
        [InlineData(1, 2)]
        public void Dado_Dois_Paginations_Quando_Possuirem_O_Mesmo_Valor_E_Forem_Comparadas_Entao_Devera_Resultar_Verdadeiro(int page, int size)
        {
            var pagination1 = new Pagination(page, size);
            var pagination2 = new Pagination(page, size);

            pagination1.Should().Be(pagination2);
            (pagination1 == pagination2).Should().BeTrue();
            (pagination1 != pagination2).Should().BeFalse();
            pagination1.GetHashCode().Should().Be(pagination2.GetHashCode());
            pagination1.GetValue().Should().Be(pagination2.GetValue());
            pagination1.Skip.Should().Be(pagination2.Skip);
        }
    }
}
