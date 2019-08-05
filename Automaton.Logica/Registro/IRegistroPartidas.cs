using System.Collections.Generic;

namespace Automaton.Logica.Registro
{
    public interface IRegistroPartidas
    {
        RegistroPartidaCompletaDto ObtenerPartida(int idPartida);

        IEnumerable<RegistroPartidaDto> ObtenerUltimasPartidas();
    }
}
