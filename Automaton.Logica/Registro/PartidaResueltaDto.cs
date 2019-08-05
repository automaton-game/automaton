using Automaton.Contratos.Entorno;
using System.Collections.Generic;

namespace Automaton.Logica.Registro
{
    public class PartidaResueltaDto : PartidaDto
    {
        public ICollection<Tablero> Tableros { get; set; }

        public string MotivoDerrota { get; set; }
    }
}
