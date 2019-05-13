using Automaton.Contratos.Entorno;

namespace Automaton.Contratos.Robots
{
    public interface IRobot
    {
        AccionRobotDto GetAccionRobot(IConsole console);

        Tablero Tablero { get; set; }
    }
}
