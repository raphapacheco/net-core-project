using BackEnd.NetCore.Usuario.Commons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.NetCore.Usuario.Common.Maps
{
    internal class UsuarioMap : IEntityTypeConfiguration<UsuarioDAO>
    {
        public void Configure(EntityTypeBuilder<UsuarioDAO> builder)
        {            
            builder.ToTable("usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Nome)
                .HasColumnName("nome");

            builder.Property(x => x.Login)
                .HasColumnName("login");

            builder.Property(x => x.Email)
                .HasColumnName("email");

            builder.Property(x => x.Senha)
                .HasColumnName("senha");

            builder.Property(x => x.CPF)
                .HasColumnName("cpf");

            builder.Property(x => x.CNPJ)
                .HasColumnName("cnpj");

            builder.Property(x => x.Celular)
                .HasColumnName("celular");

            builder.Property(x => x.DataCadastro)
                .HasColumnName("datacadastro")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Ativo)
                .HasColumnName("ativo");

            builder.Property(x => x.Bloqueado)
                .HasColumnName("bloqueado");
        }
    }
}