namespace Automaton.Contratos.Robots
{
    /// <summary>
    /// Interfaz de consola
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Accion que permite enviar texto a la pantalla con el fin de poder depurar la logica del robot.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void WriteLine(string format, params object[] args);
    }
}
