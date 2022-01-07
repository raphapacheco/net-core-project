using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using BackEnd.NetCore.Api.Configurations;
using BackEnd.NetCore.Api.Models;
using BackEnd.NetCore.Api.Services;
using BackEnd.NetCore.Api.Services.Interfaces;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace BackEnd.NetCore.Api
{
    public class Startup
    {
        private IConfiguration Configuracao { get; }

        public Startup(IConfiguration configuration)
        {
            Configuracao = configuration;
        }

        [ExcludeFromCodeCoverage]
        private static void AplicarConfiguracoesEspecificasDoAmbienteDeProducao(IWebHostEnvironment env, IApplicationBuilder app)
        {
            if (!env.IsDevelopment())
            {
                //Esse código foi colocado aqui pois fica redirecionando para HTTPS, 
                //dessa forma o front não enxerga o back no ambiente de desenvolvimento
                app.UseHttpsRedirection();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConfiguracaoToken>(options => Configuracao.GetSection(ConfiguracaoToken.Secao).Bind(options));
            services.AddCors(options => { options.AddDefaultPolicy(builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }); });
            services.AddControllers();

            services
              .AddSingleton(Configuracao)
              .AddSingleton<ITokenService, TokenService>()
              .AddSingleton<IConfigureNamedOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>()              
              .AddDbContext<UsuarioContext>(options => options.UseNpgsql(Configuracao.GetConnectionString("DEFAULT")))              
              .AddMediatR(AppDomain.CurrentDomain.Load("BackEnd.NetCore.Usuario"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "BackEnd NetCore Api v1", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.ConfigureOptions<ConfigureJwtBearerOptions>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            AplicarConfiguracoesEspecificasDoAmbienteDeProducao(env, app);

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd NetCore Api"));

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
