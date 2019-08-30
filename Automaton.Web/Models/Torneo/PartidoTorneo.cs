using System.Collections.Generic;

namespace Automaton.Web.Models.Torneo
{
    public class PartidoTorneo
    {
        public int? Id { get; set; }

        public IList<string> Jugadores { get; set; }

        public string Ganador { get; set; }

        public short PorcentajeProgreso { get; set; }
    }
}
