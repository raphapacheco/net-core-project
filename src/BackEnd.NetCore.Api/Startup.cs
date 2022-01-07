using System;
using System.IO;
using System.Reflection;
using BackEnd.NetCore.Api.Configurations;
using BackEnd.NetCore.Api.Models;
using BackEnd.NetCore.Api.Services;
using BackEnd.NetCore.Api.Services.Interfaces;
using BackEnd.NetCore.Usuario.Commons.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MediatR;

namespace BackEnd.NetCore.Api
{
    public class Startup
    {
        private IConfiguration Configuracao { get; }

        public Startup(IConfiguration configuration)
        {
            Configuracao = configuration;
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConfiguracaoToken>(options => Configuracao.GetSection(ConfiguracaoToken.Secao).Bind(options));
            services.AddCors(options => { options.AddDefaultPolicy(builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }); });
            services.AddControllers();
            
            services
                .AddSingleton(Configuracao)
                .AddSingleton<ITokenService, TokenService>()
                .AddSingleton<IConfigureNamedOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>()
                //.AddSingleton<IConfiguracaoSqlServer, ConfiguracaoSqlServer>(provider => _configuracaoSqlServer)
                .AddDbContext<UsuarioContext>(options => options.UseNpgsql(Configuracao.GetConnectionString("Padrao")));

            services.AddMediatR(AppDomain.CurrentDomain.Load("BackEnd.NetCore.Usuario"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Alterdata Heimdall", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            services.ConfigureOptions<ConfigureJwtBearerOptions>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alterdata Heimdall v1"));
            }
            else
            {
                //Esse código foi colocado aqui pois fica redirecionando para HTTPS, 
                //dessa forma o front não enxerga o back no ambiente de desenvolvimento
                app.UseHttpsRedirection();
            }

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
