using Automaton.Contratos.Entorno;

namespace Automaton.Contratos.Robots
{
    public interface IRobot
    {
        AccionRobotDto GetAccionRobot();

        Tablero Tablero { get; set; }
    }
}
