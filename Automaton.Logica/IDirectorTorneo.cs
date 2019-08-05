using Automaton.Logica.Registro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public interface IDirectorTorneo
    {
        Task<PartidaResueltaDto> Iniciar(ICollection<LogicaRobotDto> logicaRobotDtos);
    }
}