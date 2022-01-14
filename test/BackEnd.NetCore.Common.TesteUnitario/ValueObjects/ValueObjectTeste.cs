using BackEnd.NetCore.Common.ValueObjects;
using FluentAssertions;
using BackEnd.NetCore.Common.TesteUnitario.ValueObjects.Stubs;
using Xunit;

namespace BackEnd.NetCore.Common.TesteUnitario.ValueObjects
{
    [Trait("Category", "TesteUnitario")]
    [Trait("Class", nameof(ValueObject))]
    public class ValueObjectTeste
    {
        [Theory(DisplayName = @"Dado dois ValueObjects com o mesmo valor, quando forem comparados, então deverá resultar verdadeiro")]
        [InlineData("123456")]
        [InlineData("000012")]
        public void Dado_Dois_ValueObjects_Com_O_Mesmo_Valor_Quando_Forem_Comparados_Entao_Devera_Resultar_Verdadeiro(string valor)
        {
            var codigo1 = new Codigo(valor);
            var codigo2 = new Codigo(valor);

            codigo1.Should().Be(codigo2);
            (codigo1 == codigo2).Should().BeTrue();
            (codigo1 != codigo2).Should().BeFalse();
        }

        [Theory(DisplayName = @"Dado dois ValueObjects com valores distintos, quando forem comparados, então deverá resultar falso")]
        [InlineData("000001", "000002")]
        [InlineData("999999", "666666")]
        public void Dado_Dois_ValueObjects_Com_Valores_Distintos_Quando_Forem_Comparados_Entao_Devera_Resultar_Falso(string valor1, string valor2)
        {
            var codigo1 = new Codigo(valor1);
            var codigo2 = new Codigo(valor2);

            codigo1.Should().NotBe(codigo2);
            (codigo1 == codigo2).Should().BeFalse();
            (codigo1 != codigo2).Should().BeTrue();
        }

        [Theory(DisplayName = @"Dado dois ValueObjects onde um destes esteja com seu valor nulo, quando forem comparados, então deverá resultar falso")]
        [InlineData("000001", null)]
        [InlineData(null, "000001")]
        public void Dado_Dois_ValueObjects_Onde_Um_Destes_Esteja_Com_Seu_Valor_Nulo_Quando_Forem_Comparados_Entao_Devera_Resultar_Falso(string valor1, string valor2)
        {
            var codigo1 = new Codigo(valor1);
            var codigo2 = new Codigo(valor2);

            codigo1.Should().NotBe(codigo2);
            (codigo1 == codigo2).Should().BeFalse();
            (codigo1 != codigo2).Should().BeTrue();
        }

        [Theory(DisplayName = @"Dado um ValueObject, quando este for instanciado, então este deve ter seu valor corretamente preenchido")]
        [InlineData("000001")]
        [InlineData("100002")]
        [InlineData("999999")]
        [InlineData("0123456789")]
        public void Dado_Um_Value_Object_Quando_Este_For_Instanciado_Entao_Este_deve_Ter_Seu_Valor_Corretamente_Preenchido(string valor)
        {
            var codigo = new Codigo(valor);

            codigo.Should().NotBeNull();
            codigo.Valor.Should().Be(valor);
            codigo.GetValue().Should().Be(codigo.Valor);
        }

        [Theory(DisplayName = @"Dado um ValueObject com mais de um componente de comparação, quando o hashcode for solicitado, então o hashcode de seus componentes deverão compor seu hashcode")]
        [InlineData(21, "2222-1122")]
        [InlineData(24, "2211-2233")]
        public void Dado_Um_ValueObject_Com_Mais_De_Um_Componente_De_Comparacao_Quando_O_Hashcode_For_Solicitado_Entao_O_Hashcode_De_Seus_Componentes_Deverao_Compor_Seu_Hashcode(int ddd, string numero)
        {
            var telefone = new Telefone(ddd, numero);

            telefone.GetHashCode().Should().Be(ddd.GetHashCode() ^ numero.GetHashCode());
            telefone.GetValue().Should().Be(telefone.ToString());
        }

        [Fact(DisplayName = @"Dado um ValueObject, quando comparado com um objeto nulo, então deverá resultar falso")]
        public void Dado_Um_ValueObject_Quando_Comparado_Com_Um_Objeto_Nulo_Entao_Devera_Resultar_Falso()
        {
            var codigo = new Codigo("000001");
            codigo.Equals((object)null).Should().BeFalse();
        }

        [Fact(DisplayName = @"Dado um ValueObject, quando ele é comparado com outro ValueObject nulo, então deverá resultar falso")]
        public void Dado_Um_ValueObject_Quando_Ele_E_Comparado_Com_Outro_ValueObject_Nulo_Entao_Devera_Resultar_Falso()
        {
            var codigo1 = new Codigo("000001");
            Codigo codigo2 = null;

            codigo1.Equals(codigo2).Should().BeFalse();
            (codigo1 == codigo2).Should().BeFalse();
            (codigo1 != codigo2).Should().BeTrue();
        }

        [Fact(DisplayName = @"Dado um ValueObject, quando for a mesma referência, então deverá resultar verdadeiro")]
        public void Dado_Um_ValueObject_Quando_For_A_Mesma_Referência_Entao_Devera_Resultar_Verdadeiro()
        {
            var codigo = new Codigo("000001");
            codigo.Equals((object)codigo).Should().BeTrue();
            codigo.Equals(codigo).Should().BeTrue();
        }

        [Fact(DisplayName = @"Dado um ValueObject, quando seu valor é nulo e este for comparado com nulo, então deverá resultar verdadeiro")]
        public void Dado_Um_ValueObject_Quando_Seu_Valor_Nulo_E_Este_For_Comparado_Com_Nulo_Entao_Devera_Resultar_Verdadeiro()
        {
            Codigo codigoStub = null;

            (codigoStub == null).Should().BeTrue();
        }

        [Theory(DisplayName = @"Dado um ValueObject, quando seu valor é nulo, então seu hashcode deve ser zero (0)")]
        [InlineData(null)]
        public void Dado_Um_ValueObject_Quando_Seu_Valor_Nulo_Entao_Seu_Hashcode_Deve_Ser_Zero(string value)
        {
            var codigo = new Codigo(value);

            codigo.GetHashCode().Should().Be(0);
        }
    }
}
