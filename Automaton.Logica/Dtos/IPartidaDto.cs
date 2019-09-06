using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public interface IPartidaDto
    {
        /// <summary>
        /// Jugadores involucrados en la partida.
        /// </summary>
        IList<string> Jugadores { get; set; }
    }
}
