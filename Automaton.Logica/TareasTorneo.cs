using Automaton.Logica.Registro;
using System;
using System.Collections.Generic;

namespace Automaton.Logica
{
    public class TareasTorneo : ITareasTorneo
    {
        public event EventHandler<RegistroPartidaResueltaDto> PartidaResuelta;

        public RegistroPartidaEnCursoDto IniciarPartida(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            return null;
        }
    }
}
