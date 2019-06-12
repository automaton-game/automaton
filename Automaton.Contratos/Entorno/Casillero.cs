using Automaton.Contratos.Robots;
using System.Collections.Generic;

namespace Automaton.Contratos.Entorno
{
    /// <summary>
    /// Representa una cuadricula del tablero
    /// </summary>
    public class Casillero
    {
        /// <summary>
        /// Nro de fila comenzando en cero.
        /// </summary>
        public int NroFila { get; set; }

        public int NroColumna { get; set; }

        public IList<IRobot> Robots { get; set; }

        public IRobot Muralla { get; set; }

        public FilaTablero Fila { get; set; }
    }
}
