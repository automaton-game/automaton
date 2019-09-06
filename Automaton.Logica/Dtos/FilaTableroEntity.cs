using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class FilaTableroEntity
    {
        public int NroFila { get; set; }

        public IList<CasilleroEntity> Casilleros { get; set; }
    }
}
