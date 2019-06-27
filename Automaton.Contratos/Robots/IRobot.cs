using Automaton.Contratos.Entorno;

namespace Automaton.Contratos.Robots
{
    /// <summary>
    /// Interfaz que debe implementar un robot
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        /// Accion que devuelve un robot turno a turno
        /// </summary>
        /// <param name="console"></param>
        /// <returns></returns>
        AccionRobotDto GetAccionRobot(IConsole console);

        /// <summary>
        /// Contiene la informacion del tablero para el turno actual.
        /// </summary>
        Tablero Tablero { get; set; }
    }
}
