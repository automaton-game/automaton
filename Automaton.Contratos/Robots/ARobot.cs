using Automaton.Contratos.Entorno;

namespace Automaton.Contratos.Robots
{
    public abstract class ARobot : IRobot
    {
        public Tablero Tablero { get; set; }

        public abstract AccionRobotDto GetAccionRobot(IConsole console);
    }
}
