using System.Collections.Generic;

namespace Automaton.Logica.Registro
{
    public interface IRegistroPartidas
    {
        RegistroPartidaResueltaDto ObtenerPartida(int idPartida);

        IEnumerable<IRegistroPartidaDto> ObtenerUltimasPartidas();
    }
}
