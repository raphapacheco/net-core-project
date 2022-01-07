using BackEnd.NetCore.Usuario.Common.Maps;
using BackEnd.NetCore.Usuario.Commons.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.NetCore.Usuario.Commons.Contexts
{
   public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        { }                     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new UsuarioMap());                    

            base.OnModelCreating(modelBuilder);
        }
    }
}

