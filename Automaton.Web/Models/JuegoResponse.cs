using System.Collections.Generic;

namespace Automaton.Web.Models
{
    public class JuegoResponse
    {
        public ICollection<Tablero> Tableros { get; set; }

        public string Ganador { get; set; }

        public string MotivoDerrota { get; set; }
    }
}
