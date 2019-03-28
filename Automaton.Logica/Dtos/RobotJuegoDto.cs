using Automaton.Contratos.Robots;
using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class RobotJuegoDto
    {
        public string Usuario { get; set; }

        public IRobot Robot { get; set; }

        public List<AccionRobotDto> Acciones { get; set; } = new List<AccionRobotDto>();
    }
}
