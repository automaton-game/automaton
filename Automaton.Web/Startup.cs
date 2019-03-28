using AutoMapper;
using Automaton.Compilador;
using Automaton.Contratos.Entorno;
using Automaton.Contratos.Helpers;
using Automaton.Logica;
using Automaton.Web.Logica;
using Automaton.Web.MappingProfiles;
using Automaton.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

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

            services.AddTransient(p => {
                var config = new MapperConfiguration(cfg => {
                    cfg.AddProfile<ErrorProfile>();
                    cfg.CreateMap<Tablero, Models.Tablero>();
                    cfg.CreateMap<FilaTablero, Models.FilaTablero>();
                    cfg.CreateMap<Casillero, Models.Casillero>()
                        .ForMember(m => m.Muralla, y => y.MapFrom(m => m.Muralla != null ? (int?)m.Muralla.GetHashCode() : null))
                        .ForMember(m => m.Robots, y => y.MapFrom(m => m.Robots != null ? m.Robots.Count : 0))
                        .ForMember(m => m.RobotDuenio, y => y.MapFrom(m => m.ObtenerRobotLider() != null ? (int?)m.ObtenerRobotLider().GetHashCode() : null));
                        ;
                });

                var mapper = config.CreateMapper();
                return mapper;
            });

            services.AddTransient<IJuego2v2, Juego2v2>();
            services.AddTransient<IFabricaTablero, FabricaTablero>();
            services.AddTransient<IFabricaRobot, FabricaRobot>();
            services.AddTransient<ErrorHandlingMiddleware>();

            services.AddScoped<ITempFileManager, TempFileManager>();
            services.AddScoped<IDomainFactory, DomainFactory>();
            services.AddLogging(ConfigureLogging);

            services.AddSingleton<IRegistroRobots, RegistroRobots>();
            services.AddSingleton<IRegistroJuegosManuales, RegistroJuegosManuales>();
            services.AddSingleton<IMetadataFactory, MetadataFactory>();
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
