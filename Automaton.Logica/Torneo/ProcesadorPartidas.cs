using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo
{
    public class ProcesadorPartidas : IProcesadorPartidas
    {
        private readonly IDirectorTorneo directorTorneo;
        private readonly IRegistroNotificador registroNotificador;
        private readonly IRegistroPartidasDao registroPartidasDao;
        private readonly ITareasTorneo tareasTorneo;

        public ProcesadorPartidas(
            IDirectorTorneo directorTorneo,
            IRegistroNotificador registroNotificador,
            IRegistroPartidasDao registroPartidasDao,
            ITareasTorneo tareasTorneo
            )
        {
            this.directorTorneo = directorTorneo;
            this.registroNotificador = registroNotificador;
            this.registroPartidasDao = registroPartidasDao;
            this.tareasTorneo = tareasTorneo;
        }

        public async Task ProcesarAsync(CancellationToken cancellationToken)
        {
            var logicaRobotDtos = await tareasTorneo.ObtenerLogicas(cancellationToken);
            if (logicaRobotDtos == null)
            {
                return;
            }

            // Creo el registro
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
    }
}
