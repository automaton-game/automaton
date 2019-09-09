using System.Collections.Generic;

namespace Automaton.Logica.Dtos.Model
{
    public class JuegoResponse
    {
        public ICollection<TableroModel> Tableros { get; set; }

        public string Ganador { get; set; }

        public string MotivoDerrota { get; set; }
    }
}
