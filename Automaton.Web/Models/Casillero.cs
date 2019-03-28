
using System.Collections.Generic;

namespace Automaton.Web.Models
{
    public class Casillero
    {
        public int NroFila { get; set; }

        public int NroColumna { get; set; }

        public int Robots { get; set; }

        public string RobotDuenio { get; set; }

        public string Muralla { get; set; }
    }
}
