using System.Collections.Generic;

namespace Automaton.Web.Models
{
    public class FilaTablero
    {
        public int NroFila { get; set; }

        public IList<Casillero> Casilleros { get; set; }
    }
}
