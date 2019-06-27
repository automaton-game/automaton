using System.Collections.Generic;

namespace Automaton.Contratos.Entorno
{
    /// <summary>
    /// Representa una fila completa del tablero. Contiene los casilleros para esa fila.
    /// </summary>
    public class FilaTablero
    {
        /// <summary>
        /// Nro de fila comenzando por 1
        /// </summary>
        public int NroFila { get; set; }

        /// <summary>
        /// Casilleros que contiene la fila
        /// </summary>
        public IList<Casillero> Casilleros { get; set; }

        /// <summary>
        /// Hace referencia al tablero al que pertenece la fila
        /// </summary>
        public Tablero Tablero { get; set; }
    }
}
