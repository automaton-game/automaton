using Automaton.Logica.Dtos;
using System.Collections.Generic;

namespace Automaton.Logica.Registro
{
    public class PartidaResueltaDto : PartidaDto
    {
        public ICollection<TableroLogico> Tableros { get; set; }

        public string MotivoDerrota { get; set; }
    }
}
