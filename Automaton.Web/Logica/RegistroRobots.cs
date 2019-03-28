using System;
using System.Collections.Generic;
using System.Linq;

namespace Automaton.Web.Logica
{
    public class RegistroRobots : IRegistroRobots
    {
        private IDictionary<string, Tuple<string, int, DateTime>> lista = new Dictionary<string, Tuple<string, int, DateTime>>(); 

        public IDictionary<string, int> ObtenerResumen()
        {
            return lista.ToDictionary(d => d.Key, d => d.Value.Item2);
        }

        public string ObtenerUltimoCampeon()
        {
            return lista.Where(d => d.Value.Item2 > 0).OrderByDescending(d => d.Value.Item3).Select(d => d.Value.Item1).FirstOrDefault();
        }

        public void Registrar(string key, string logica)
        {
            lista[key] = new Tuple<string, int, DateTime>(logica, 0, DateTime.Now);
        }

        public int RegistrarVictoria(string key)
        {
            if (lista.TryGetValue(key, out Tuple<string, int, DateTime> tupla))
            {
                lista[key] = new Tuple<string, int, DateTime>(tupla.Item1, tupla.Item2 + 1, tupla.Item3);
                return tupla.Item2 + 1;
            }

            return 0;
        }
    }
}
