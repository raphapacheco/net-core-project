using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using BackEnd.NetCore.Common.ValueObjects;
using BackEnd.NetCore.Common.TesteUnitario.ValueObjects.Stubs;
using System.Linq;

namespace BackEnd.NetCore.Common.TesteUnitario.ValueObjects
{
    [Trait("Category", "TesteUnitario")]
    [Trait("Class", nameof(Pagination))]
    public class PaginationResponseTeste
    {
        [Fact(DisplayName = @"Dado dois PaginationResponses, quando possuirem o mesmo valor e forem comparadas, então deverá resultar verdadeiro")]
        public void Dado_Dois_PaginationResponses_Quando_Possuirem_O_Mesmo_Valor_E_Forem_Comparadas_Entao_Devera_Resultar_Verdadeiro()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario("OWNER", "123456"),
                new Usuario("MANAGER", "654321")
            };

            if(usuarios.FirstOrDefault().Valido(out _))
            {            
                var paginationResponse1 = new PaginationResponse<Usuario>(usuarios, 1);
                var paginationResponse2 = new PaginationResponse<Usuario>(usuarios, 1);

                paginationResponse1.Should().Be(paginationResponse2);
                (paginationResponse1 == paginationResponse2).Should().BeTrue();
                (paginationResponse1 != paginationResponse2).Should().BeFalse();
                paginationResponse1.GetHashCode().Should().Be(paginationResponse2.GetHashCode());
                paginationResponse1.GetValue().Should().Be(paginationResponse2.GetValue());
                paginationResponse1.Count.Should().Be(2);
            }
        }
    }
}
