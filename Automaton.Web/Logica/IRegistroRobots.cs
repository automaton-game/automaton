using System.Collections.Generic;

namespace Automaton.Web.Logica
{
    public interface IRegistroRobots
    {
        int RegistrarVictoria(string ganador, string logicaGanador = null);

        RegistroVictoriasDto ObtenerUltimoCampeon();

        IDictionary<string, int> ObtenerResumen();

        void BorrarTodo();
    }
}
