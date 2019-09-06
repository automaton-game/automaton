using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class TableroEntity
    {
        public ICollection<FilaTableroEntity> Filas { get; set; }

        public IList<string> Consola { get; set; }

        public string TurnoRobot { get; set; }
    }
}
