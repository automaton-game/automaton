using System.Collections.Generic;

namespace Automaton.Contratos.Entorno
{
    public interface IFilaTablero
    {
        ICollection<ICasillero> Casilleros { get; set; }
        int NroFila { get; set; }
        ITablero Tablero { get; set; }
    }
}