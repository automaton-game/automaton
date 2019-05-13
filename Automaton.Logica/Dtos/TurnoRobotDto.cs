using Automaton.Contratos.Robots;
using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class TurnoRobotDto : ResultadoTurnoDto
    {
        public AccionRobotDto Accion { get; set; }

        public IList<string> Consola { get; set; }
    }
}
