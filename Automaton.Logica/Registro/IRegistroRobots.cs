using System.Collections.Generic;

namespace Automaton.Logica.Registro
{
    public interface IRegistroRobots
    {
        void RegistrarVictoria(string ganador, string perdedor, string logicaGanador = null);

        KeyValuePair<string, string>? ObtenerLogicaCampeon();

        IDictionary<string, int> ObtenerResumen();

        void BorrarTodo();
    }
}
