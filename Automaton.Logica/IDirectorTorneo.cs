using Automaton.Logica.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public interface IDirectorTorneo
    {
        PartidaResueltaDto ResolverPartida(ICollection<LogicaRobotDto> logicaRobotDtos);

        Task<PartidaResueltaDto> ResolverPartidaAsync(ICollection<LogicaRobotDto> logicaRobotDtos);
    }
}