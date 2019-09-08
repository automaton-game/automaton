using Automaton.Contratos.Entorno;
using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class FilaTableroDto : IFilaTablero
    {
        public int NroFila { get; set; }

        public ICollection<ICasillero> Casilleros { get; set; }

        public ITablero Tablero { get; set; }
    }
}
