using BackEnd.NetCore.Usuario.Commons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.NetCore.Usuario.Common.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioDAO>
    {
        public void Configure(EntityTypeBuilder<UsuarioDAO> builder)
        {            
            builder.ToTable("usuarioapp");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id");

            builder.Property(x => x.Nome)
                .HasColumnName("Nome");

            builder.Property(x => x.Login)
                .HasColumnName("Login");

            builder.Property(x => x.Email)
                .HasColumnName("Email");

            builder.Property(x => x.Senha)
                .HasColumnName("Senha");

            builder.Property(x => x.CPF)
                .HasColumnName("CPF");

            builder.Property(x => x.CNPJ)
                .HasColumnName("CNPJ");

            builder.Property(x => x.Celular)
                .HasColumnName("Celular");

            builder.Property(x => x.DataCadastro)
                .HasColumnName("DataCadastro");

            builder.Property(x => x.Ativo)
                .HasColumnName("Ativo");

            builder.Property(x => x.Bloqueado)
                .HasColumnName("Bloqueado");
        }
    }
}