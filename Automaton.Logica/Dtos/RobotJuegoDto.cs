using Automaton.Contratos.Robots;
using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class RobotJuegoDto
    {
        public string Usuario { get; set; }

        public IRobot Robot { get; set; }

        public List<ResultadoTurnoDto> Turnos { get; set; } = new List<ResultadoTurnoDto>();
    }
}
