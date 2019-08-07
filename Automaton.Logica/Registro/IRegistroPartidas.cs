using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica.Registro
{
    public interface IRegistroPartidas
    {
        PartidaResueltaDto ObtenerPartida(int idPartida);

        /// <summary>
        /// Devuelve las partidas con las ultimas versiones de cada jugador. Filtrando por el jugador actual.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        IEnumerable<IRegistroPartidaDto> ObtenerUltimasPartidas(string usuario);

        /// <summary>
        /// Devuelve las partidas con las ultimas versiones de cada jugador
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        IEnumerable<IRegistroPartidaDto> ObtenerUltimasPartidas();

        /// <summary>
        /// Cada vez que un jugador modifica su logica, esta registrando un nuevo robot con su mismo usuario.
        /// </summary>
        /// <returns>
        /// Devuelve una tarea que finaliza cuando todas las tareas asociadas al registro finalizan.
        /// </returns>
        Task RegistrarRobotAsync(LogicaRobotDto logicaRobotDto);
    }
}
