using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public class TareasTorneo : ITareasTorneo
    {
        public Task<RegistroPartidaEnCursoDto> IniciarPartida(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            return null;
        }
    }
}
