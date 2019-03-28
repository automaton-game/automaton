using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;

namespace Automaton.Logica.Robots
{
    public class RobotManual : IRobot
    {
        public Tablero Tablero { get; set; }

        public AccionRobotDto AccionRobot { get; set; }

        public AccionRobotDto GetAccionRobot()
        {
            return AccionRobot;
        }
    }
}
