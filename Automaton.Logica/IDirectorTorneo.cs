using Automaton.Logica.Registro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public interface IDirectorTorneo
    {
        PartidaResueltaDto Iniciar(ICollection<LogicaRobotDto> logicaRobotDtos);

        Task<PartidaResueltaDto> IniciarPartidaAsync(ICollection<LogicaRobotDto> logicaRobotDtos);
    }
}