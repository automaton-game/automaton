using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public class TareasTorneo : ITareasTorneo
    {
        private readonly IDirectorTorneo directorTorneo;
        private readonly IRegistroNotificador registroNotificador;
        private readonly IRegistroPartidasDao registroPartidasDao;

        public TareasTorneo(
            IDirectorTorneo directorTorneo,
            IRegistroNotificador registroNotificador,
            IRegistroPartidasDao registroPartidasDao)
        {
            this.directorTorneo = directorTorneo;
            this.registroNotificador = registroNotificador;
            this.registroPartidasDao = registroPartidasDao;
        }

        private static IList<Task> partidasEnCurso = new List<Task>();

        public async Task<RegistroPartidaEnCursoDto> IniciarPartida(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            var registroPartidaEnCursoDto = await registroPartidasDao.Create<RegistroPartidaEnCursoDto>();
            var partidaEnCurso = directorTorneo
                .IniciarPartidaAsync(logicaRobotDtos)
                .ContinueWith(p =>
                {
                    var partidaResuelta = p.Result;
                    var updateRegistro = registroPartidasDao.Update<RegistroPartidaResueltaDto>(registroPartidaEnCursoDto.IdPartida);
                    updateRegistro.Wait();
                    var registroPartida = updateRegistro.Result;

                    registroPartida.Ganador = partidaResuelta.Ganador;
                    registroPartida.Jugadores = partidaResuelta.Jugadores;
                    registroPartida.MotivoDerrota = partidaResuelta.MotivoDerrota;
                    registroPartida.Tableros = partidaResuelta.Tableros;
                    registroPartida.PorcentajeProgreso = 100;
                });

            partidasEnCurso.Add(partidaEnCurso);
            _ = partidaEnCurso.ContinueWith(t => partidasEnCurso.Remove(partidaEnCurso));

            return registroPartidaEnCursoDto;
        }
    }
}
