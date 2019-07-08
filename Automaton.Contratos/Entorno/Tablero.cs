using Automaton.Contratos.Robots;
using System.Collections.Generic;

namespace Automaton.Contratos.Entorno
{
    /// <summary>
    /// Hace referencia al tablero completo del juego. Contiene toda la informacion del tablero correspondiente al turno actual.
    /// </summary>
    public class Tablero
    {
        /// <summary>
        /// Almacena las filas del tablero
        /// </summary>
        public IList<FilaTablero> Filas { get;set; }

        /// <summary>
        /// Hace refencia al turno del robot para el tablero actual. 
        /// </summary>
        public IRobot TurnoRobot { get; set; }
    }
}
