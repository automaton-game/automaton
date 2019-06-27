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
        /// Nro de fila comenzando en 1.
        /// </summary>
        public int NroFila { get; set; }

        /// <summary>
        /// Nro de columna comenzando en 1
        /// </summary>
        public int NroColumna { get; set; }

        /// <summary>
        /// Lista de robots que contiene la celda
        /// </summary>
        public IList<IRobot> Robots { get; set; }

        /// <summary>
        /// Si el casillero tiene una muralla, tendrá cargado el una referencia al robot que la haya construido. Si no tiene muralla, tendrá NULL
        /// </summary>
        public IRobot Muralla { get; set; }

        /// <summary>
        /// Objeto fila al que pertenece este casillero
        /// </summary>
        public FilaTablero Fila { get; set; }
    }
}
