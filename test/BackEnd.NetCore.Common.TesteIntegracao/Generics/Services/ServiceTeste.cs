using BackEnd.NetCore.Common.Generics.Repositories;
using BackEnd.NetCore.Common.Generics.Services;
using BackEnd.NetCore.Common.ValueObjects;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using BackEnd.NetCore.Usuario.Commons.Models;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace BackEnd.NetCore.Common.TesteIntegracao.Generics.Services
{
    public class ServiceTeste
    {
        private DbContextOptionsBuilder<UsuarioContext> _builder;
        private UsuarioContext _contexto;
        private Repository<UsuarioDAO> _repositorio;
        private Service<UsuarioDAO> _servico;

        public ServiceTeste()
        {            
            _builder = new DbContextOptionsBuilder<UsuarioContext>()
                .UseNpgsql("Server=localhost;Port=5434;Database=postgres;User Id=postgres;Password = 123456;");

            _contexto = new UsuarioContext(_builder.Options);
            _repositorio = new Repository<UsuarioDAO>(_contexto);
            _servico = new Service<UsuarioDAO>(_repositorio);
        }

        [Fact(DisplayName = @"Dado o método InserirAsync, quando passado um modelo inválido, deve lançar exceção")]
        public async void Dado_O_Metodo_InserirAsync_Quando_Passado_Um_Modelo_Invalido_Deve_Lancar_Excecao()
        {  
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _servico.InserirAsync(new UsuarioDAO()));
            exception.Message.Should().Contain("Modelo inválido");
        }

        [Fact(DisplayName = @"Dado o método AtualizarAsync, quando passado um modelo inválido, deve lançar exceção")]
        public async void Dado_O_Metodo_AtualizarAsync_Quando_Passado_Um_Modelo_Invalido_Deve_Lancar_Excecao()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _servico.AtualizarAsync(new UsuarioDAO()));
            exception.Message.Should().Contain("Modelo inválido");
        }

        [Fact(DisplayName = @"Dado o método ConsultarPorExpressaoAsync, quando executado, deve localizar corretaemtne as informações")]
        public async void Dado_O_Metodo_ConsultarPorExpressaoAsync_Quando_Executado_Deve_localizar_corretaemtne_as_informacoes()
        {
            var consulta = await _servico.ConsultarPorExpressaoAsync(x => x.Login.Equals("OWNER"));
            consulta.Should().NotBeNull();
        }

        [Fact(DisplayName = @"Dado o método ConsultarTodosAsync, quando passado um modelo inválido, deve lançar exceção")]
        public async void Dado_O_Metodo_ConsultarTodosAsync_Quando_Passado_Um_Modelo_Invalido_Deve_Lancar_Excecao()
        {
            var consulta = await _servico.ConsultarTodosAsync(new Pagination(1,2));
            consulta.Should().NotBeNull();
            consulta.Count.Should().Be(2);
        }
    }
}
