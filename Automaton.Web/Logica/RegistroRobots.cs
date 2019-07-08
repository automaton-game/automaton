using System;
using System.Collections.Generic;
using System.Linq;

namespace Automaton.Web.Logica
{
    public class RegistroRobots : IRegistroRobots
    {
        private IList<RegistroVictoriasDto> victorias = new List<RegistroVictoriasDto>();

        public void BorrarTodo()
        {
            victorias = new List<RegistroVictoriasDto>();
        }

        public IDictionary<string, int> ObtenerResumen()
        {
            return victorias.GroupBy(v => v.Usuario).ToDictionary(d => d.Key, d => d.Sum(v => v.Victorias));
        }

        public RegistroVictoriasDto ObtenerUltimoCampeon()
        {
            var a = victorias.LastOrDefault();
            return a;
        }

        public int RegistrarVictoria(string ganador, string logicaGanador = null)
        {
            var ultimo = ObtenerUltimoCampeon();
            var nroVictorias = 1;
            if(ultimo != null)
            {
                nroVictorias = ultimo.Victorias+ 1;
            }

            if (ultimo?.Usuario == ganador)
            {
                ultimo.Victorias = nroVictorias;
            }
            else
            {
                victorias.Add(new RegistroVictoriasDto
                {
                    Fecha = DateTime.Now,
                    Victorias = nroVictorias,
                    Logica = logicaGanador,
                    Usuario = ganador
                });
            }

            return nroVictorias;
        }
    }
}
