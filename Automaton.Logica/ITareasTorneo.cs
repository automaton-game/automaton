using Automaton.Logica.Registro;
using System;
using System.Collections.Generic;

namespace Automaton.Logica
{
    public interface ITareasTorneo
    {
        event EventHandler<RegistroPartidaResueltaDto> PartidaResuelta;

        RegistroPartidaEnCursoDto IniciarPartida(ICollection<LogicaRobotDto> logicaRobotDtos);
    }
}
