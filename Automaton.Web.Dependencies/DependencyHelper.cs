using Automaton.Compilador;
using Automaton.Logica;
using Automaton.Logica.Factories;
using Automaton.Logica.Registro;
using Automaton.Logica.Torneo;
using Automaton.Workers;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tools.Documentador;

namespace Automaton.Web.Dependencies
{
    public static class DependencyHelper
    {
        public static IServiceCollection AutomatonDependencies(this IServiceCollection services)
        {
            services.AddTransient<IJuego2v2, Juego2v2>();
            services.AddTransient<IJuegoTurno, JuegoTurno>();
            
            
            services.AddTransient<IFabricaRobot, FabricaRobot>();
            services.AddTransient(f => ReaderFactory.Create());

            

            

            services.AddScoped<ITempFileManager, TempFileManager>();
            services.AddScoped<IDomainFactory, DomainFactory>();
            

            services.AddSingleton<IRegistroVictorias, RegistroVictorias>();
            
            services.AddSingleton<IMetadataFactory, MetadataFactory>();

            services.AddTransient<IRegistroPartidas, RegistroPartidas>();
            services.AddSingleton<IRegistroPartidasDao, RegistroPartidasDao>();
            services.AddSingleton<IRegistroJugadoresDao, RegistroJugadoresDao>();
            

            services.AddTransient<IDirectorTorneo, DirectorTorneo>();
            services.AddTransient<IFabricaRobotAsync, FabricaRobotAsync>();
            services.AddTransient<IJuegoFactory, JuegoFactory>();

            services.AddTransient<Func<IJuego2v2>>(x => () => x.GetService<IJuego2v2>());

            services.AddSingleton<ITareasTorneo, TareasTorneo>();
            services.AddHostedService<ProcesadorPartidasWorker>();
            services.AddTransient<IProcesadorPartidas, ProcesadorPartidas>();

            return services;
        }
    }
}
