using Automaton.Logica.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public interface ITareasTorneo
    {
        /// <summary>
        /// Devuelve una partida en curso y cuando la misma finaliza, es automaticamente notificada a la Dao
        /// </summary>
        /// <param name="logicaRobotDtos"></param>
        /// <returns></returns>
        Task RegistrarPartidaAsync(ICollection<IJugadorRobotDto> logicaRobotDtos);

        Task<ICollection<IJugadorRobotDto>> ObtenerLogicas(CancellationToken cancellationToken);
    }
}
