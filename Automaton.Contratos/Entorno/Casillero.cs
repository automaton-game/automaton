using Automaton.Contratos.Robots;
using System.Collections.Generic;

namespace Automaton.Contratos.Entorno
{
    public class Casillero
    {
        public int NroFila { get; set; }

        public int NroColumna { get; set; }

        public IList<IRobot> Robots { get; set; }

        public IRobot Muralla { get; set; }

        public FilaTablero Fila { get; set; }
    }
}
