using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;
using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class TableroDto : ITablero
    {
        public ICollection<IFilaTablero> Filas { get; set; }

        public IList<string> Consola { get; set; }

        public IRobot TurnoRobot { get; set; }
    }
}
