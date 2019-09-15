using Automaton.Logica;
using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Automaton.Workers
{
    public class ProcesadorPartidas : BackgroundService, ITareasTorneo
    {
        private readonly IDirectorTorneo directorTorneo;
        private readonly IRegistroNotificador registroNotificador;
        private readonly IRegistroPartidasDao registroPartidasDao;

        public ProcesadorPartidas(
            IDirectorTorneo directorTorneo,
            IRegistroNotificador registroNotificador,
            IRegistroPartidasDao registroPartidasDao)
        {
            this.directorTorneo = directorTorneo;
            this.registroNotificador = registroNotificador;
            this.registroPartidasDao = registroPartidasDao;
            this.logicas = new List<ICollection<LogicaRobotDto>>();
        }

        private List<ICollection<LogicaRobotDto>> logicas;
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        public override void Dispose()
        {
            semaphoreSlim.Dispose();
            base.Dispose();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SemaphoreScope(ProcesarAsync);
            }
        }

        public async Task RegistrarPartidaAsync(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            await SemaphoreScope(() => this.logicas.Add(logicaRobotDtos));
        }

        public async Task ProcesarAsync()
        {
            // Creo el registro
            var logicaRobotDtos = await SemaphoreScope(ObtenerLogicas);
            var registroPartidaEnCursoDto = await registroPartidasDao.Create<RegistroPartidaEnCursoDto>();
            registroPartidaEnCursoDto.Jugadores = logicaRobotDtos.Select(s => s.Usuario).ToArray();
            registroPartidaEnCursoDto.PorcentajeProgreso = 1;

            // Inicio la partida
            await directorTorneo
            .ResolverPartidaAsync(logicaRobotDtos)
            .ContinueWith(async p =>
            {
                // Capturo respuesta de la partida
                var partidaResuelta = p.Result;
                var registroPartida = await registroPartidasDao.Update<RegistroPartidaResueltaDto>(registroPartidaEnCursoDto.IdPartida);

                registroPartida.Ganador = partidaResuelta.Ganador;
                registroPartida.Jugadores = partidaResuelta.Jugadores;
                registroPartida.MotivoDerrota = partidaResuelta.MotivoDerrota;
                registroPartida.Tableros = partidaResuelta.Tableros;
                registroPartida.PorcentajeProgreso = 100;

                return registroPartida;
            })
            .Unwrap()

            // Notifico
            .ContinueWith(Notificar)
            .Unwrap();
        }

        private async Task<IEnumerable<IRegistroPartidaDto>> Notificar(Task<RegistroPartidaResueltaDto> registroPartidaResueltaTask)
        {
            await registroPartidaResueltaTask;
            var partidas = await registroPartidasDao.GetAll();
            await registroNotificador.NotificarUltimasPartidas(partidas);
            return partidas;
        }

        private async Task<TReturn> SemaphoreScope<TReturn>(Func<Task<TReturn>> action)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                return await action();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task SemaphoreScope(Func<Task> action)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                await action();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task SemaphoreScope(Action action)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                action();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task<TReturn> SemaphoreScope<TReturn>(Func<TReturn> action)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                return action();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private ICollection<LogicaRobotDto> ObtenerLogicas()
        {
            var logicas = this.logicas.FirstOrDefault();
            if(logicas == null)
            {
                return null;
            }

            this.logicas.Remove(logicas);

            return logicas;
        }
    }
}
