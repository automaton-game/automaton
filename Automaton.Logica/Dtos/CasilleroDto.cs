using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;
using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class CasilleroDto : ICasillero
    {
        public int NroFila { get; set; }

        public int NroColumna { get; set; }

        /// <summary>
        /// Lista de robots que contiene la celda
        /// </summary>
        public ICollection<IRobot> Robots { get; set; }

        /// <summary>
        /// Si el casillero tiene una muralla, tendrá cargado el una referencia al robot que la haya construido. Si no tiene muralla, tendrá NULL
        /// </summary>
        public IRobot Muralla { get; set; }


        public IFilaTablero Fila { get; set; }
    }
}
