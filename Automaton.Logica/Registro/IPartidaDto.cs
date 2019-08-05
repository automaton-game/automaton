using System.Collections.Generic;

namespace Automaton.Logica.Registro
{
    public interface IPartidaDto
    {
        /// <summary>
        /// Ganador de partido. Solo si esta completo el partido
        /// </summary>
        string Ganador { get; set; }

        /// <summary>
        /// Jugadores involucrados en la partida.
        /// </summary>
        IList<string> Jugadores { get; set; }
    }
}
