using Automaton.Logica.Dtos.Model;
using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class PartidaResueltaDto : PartidaDto
    {
        public ICollection<TableroModel> Tableros { get; set; }

        public string MotivoDerrota { get; set; }

        public string Ganador { get; set; }
    }
}
