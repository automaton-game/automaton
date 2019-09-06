using System.Collections.Generic;

namespace Automaton.Logica.Dtos.Model
{
    public class FilaTableroModel
    {
        public int NroFila { get; set; }

        public IList<CasilleroModel> Casilleros { get; set; }
    }
}
