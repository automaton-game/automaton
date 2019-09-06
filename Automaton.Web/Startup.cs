using AutoMapper;
using Automaton.Compilador;
using Automaton.Contratos.Entorno;
using Automaton.Contratos.Helpers;
using Automaton.Logica;
using Automaton.Logica.Factories;
using Automaton.Logica.Registro;
using Automaton.Logica.Torneo;
using Automaton.Web.Hubs;
using Automaton.Web.Logica;
using Automaton.Web.MappingProfiles;
using Automaton.Web.Middlewares;
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

            services.AddTransient<IJuego2v2, Juego2v2>();
            services.AddTransient<IJuegoTurno, JuegoTurno>();
            services.AddTransient<IDirectorJuego, DirectorJuego>();
            services.AddTransient<IFabricaTablero, FabricaTablero>();
            services.AddTransient<IFabricaRobot, FabricaRobot>();
            services.AddTransient(f => ReaderFactory.Create());

            services.AddTransient<IRegistroNotificador, RegistroNotificador>();

            services.AddTransient<ErrorHandlingMiddleware>();

            services.AddScoped<ITempFileManager, TempFileManager>();
            services.AddScoped<IDomainFactory, DomainFactory>();
            services.AddLogging(ConfigureLogging);

            services.AddSingleton<IRegistroVictorias, RegistroVictorias>();
            services.AddSingleton<IRegistroJuegosManuales, RegistroJuegosManuales>();
            services.AddSingleton<IMetadataFactory, MetadataFactory>();

            services.AddTransient<IRegistroPartidas, RegistroPartidas>();
            services.AddSingleton<IRegistroPartidasDao, RegistroPartidasDao>();
            services.AddSingleton<IRegistroJugadoresDao, RegistroJugadoresDao>();
            services.AddTransient<IRegistroNotificador, RegistroNotificador>();
            services.AddTransient<ITareasTorneo, TareasTorneo>();
            services.AddTransient<IDirectorTorneo, DirectorTorneo>();
            services.AddTransient<IFabricaRobotAsync, FabricaRobotAsync>();
            services.AddTransient<IJuegoFactory, JuegoFactory>();

            services.AddTransient<Func<IJuego2v2>>(x => () => x.GetService<IJuego2v2>());
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
