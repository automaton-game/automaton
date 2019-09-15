using AutoMapper;
using Automaton.Compilador;
using Automaton.Contratos.Entorno;
using Automaton.Contratos.Helpers;
using Automaton.Logica;
using Automaton.Logica.Factories;
using Automaton.Logica.Registro;
using Automaton.Logica.Torneo;
using Automaton.ProfileMapping;
using Automaton.Web.Dependencies;
using Automaton.Web.Hubs;
using Automaton.Web.Logica;
using Automaton.Web.MappingProfiles;
using Automaton.Web.Middlewares;
using Automaton.Workers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using Tools.Documentador;
using Tools.Documentador.TypeReaders;

namespace Automaton.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSignalR();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => {
                  options.TokenValidationParameters =
                       new TokenValidationParameters
                       {
                           ValidateIssuer = false,
                           ValidateActor = false,
                           ValidateAudience = false,
                           ValidateIssuerSigningKey = false,
                           ValidateLifetime = false,
                           ValidateTokenReplay = false,
                           
                           IssuerSigningKey = JwtTokenBuilder.GetSymmetricSecurityKey()
                       };
              });

            services.AddTransient(p => {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddProfile<ErrorProfile>();
                    cfg.AddProfile<ResultadoTurnoProfile>();
                    cfg.AddProfile<PartidaTorneoProfile>();
                    cfg.AddProfile<TableroProfile>();
                    
                });

                var mapper = config.CreateMapper();
                return mapper;
            });

            services.AddTransient<IFabricaTablero, FabricaTablero>();
            services.AddTransient<IDirectorJuego, DirectorJuego>();
            services.AddTransient<ErrorHandlingMiddleware>();
            services.AddTransient<IRegistroNotificador, RegistroNotificador>();
            services.AddSingleton<IRegistroJuegosManuales, RegistroJuegosManuales>();
            services.AddLogging(ConfigureLogging);

            services.AutomatonDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<TurnoHub>(new PathString("/turno"));
                routes.MapHub<RegistroNotificadorHub>(new PathString("/torneo"));
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        public void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddAzureWebAppDiagnostics();
        }
    }
}
