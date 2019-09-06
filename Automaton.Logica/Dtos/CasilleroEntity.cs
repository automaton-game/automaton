using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class CasilleroEntity
    {
        public int NroFila { get; set; }

        public int NroColumna { get; set; }

        /// <summary>
        /// Lista de robots que contiene la celda
        /// </summary>
        public IList<RegistroJugadorDto> Robots { get; set; }

        /// <summary>
        /// Si el casillero tiene una muralla, tendrá cargado el una referencia al robot que la haya construido. Si no tiene muralla, tendrá NULL
        /// </summary>
        public RegistroJugadorDto Muralla { get; set; }
    }
}
