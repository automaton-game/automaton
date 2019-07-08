using System.Collections.Generic;

namespace Automaton.Web.Models
{
    public class Tablero
    {
        public IList<FilaTablero> Filas { get;set; }

        public IList<string> Consola { get; set; }

        public string TurnoRobot { get; set; }
    }
}
