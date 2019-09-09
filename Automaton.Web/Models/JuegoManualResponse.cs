using Automaton.Logica.Dtos.Model;
using System.Collections.Generic;

namespace Automaton.Web.Models
{
    public class JuegoManualResponse : JuegoResponse
    {
        public ICollection<string> Jugadores { get; set; }

        public string jugadorTurnoActual { get; set; }

        public string idTablero { get; set; }
    }
}
