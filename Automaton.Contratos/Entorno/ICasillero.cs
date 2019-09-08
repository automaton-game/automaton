using System.Collections.Generic;
using Automaton.Contratos.Robots;

namespace Automaton.Contratos.Entorno
{
    public interface ICasillero
    {
        IFilaTablero Fila { get; set; }
        IRobot Muralla { get; set; }
        int NroColumna { get; set; }
        int NroFila { get; set; }
        ICollection<IRobot> Robots { get; set; }
    }
}