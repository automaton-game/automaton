using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class PartidaDto : IPartidaDto
    {
        public IList<string> Jugadores { get; set; }

        public string Ganador { get; set; }
    }
}
