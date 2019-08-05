using System.Collections.Generic;

namespace Automaton.Logica.Registro
{
    public interface IPartidaDto
    {
        string Ganador { get; set; }
        IList<string> Jugadores { get; set; }
    }
}
