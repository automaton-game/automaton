using System.Collections.Generic;

namespace Automaton.Logica.Dtos.Model
{
    public class TableroModel
    {
        public IList<FilaTableroModel> Filas { get;set; }

        public IList<string> Consola { get; set; }

        public string TurnoRobot { get; set; }
    }
}
