using System.Collections.Generic;

namespace Automaton.Web.Logica
{
    public interface IRegistroRobots
    {
        void Registrar(string key, string logica);

        int RegistrarVictoria(string key);

        string ObtenerUltimoCampeon();

        IDictionary<string, int> ObtenerResumen();
    }
}
