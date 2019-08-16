using Automaton.Logica.Dtos;
using System.Collections.Generic;
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
        Task<RegistroPartidaEnCursoDto> IniciarPartida(ICollection<LogicaRobotDto> logicaRobotDtos);
    }
}
