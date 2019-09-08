using System.Collections.Generic;
using Automaton.Contratos.Robots;

namespace Automaton.Contratos.Entorno
{
    public interface ITablero
    {
        ICollection<IFilaTablero> Filas { get; set; }

        IRobot TurnoRobot { get; set; }
    }
}