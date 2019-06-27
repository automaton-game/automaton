using Automaton.Contratos.Entorno;

namespace Automaton.Contratos.Robots
{
    /// <summary>
    /// Clase base del robot
    /// </summary>
    public abstract class ARobot : IRobot
    {
        /// <summary>
        /// Contiene la informacion del tablero en el turno actual
        /// </summary>
        public Tablero Tablero { get; set; }

        /// <summary>
        /// Metodo que implementa la logica del robot para resolver turno a turno su jugada
        /// </summary>
        /// <param name="console"></param>
        /// <returns></returns>
        public abstract AccionRobotDto GetAccionRobot(IConsole console);
    }
}
