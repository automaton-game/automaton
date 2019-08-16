using Automaton.Logica.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automaton.Logica.Registro
{
    public class RegistroVictorias : IRegistroVictorias
    {
        private IList<RegistroVictoriasDto> victorias = new List<RegistroVictoriasDto>();

        private IDictionary<string, string> logicas = new Dictionary<string, string>();

        public void BorrarTodo()
        {
            victorias = new List<RegistroVictoriasDto>();
        }

        public IDictionary<string, int> ObtenerResumen()
        {
            return victorias
                .GroupBy(v => v.Ganador)
                .Select(Agrup)
                .OrderByDescending(v => v.Value)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public KeyValuePair<string, string>? ObtenerLogicaCampeon()
        {
            var a = victorias
                .OrderByDescending(f => f.Fecha)
                .Select(ObtenerLogicaJugador)
                .FirstOrDefault();
            return a;
        }

        public void RegistrarVictoria(string ganador, string perdedor, string logicaGanador = null)
        {
            if(!string.IsNullOrEmpty(logicaGanador))
            {
                logicas[ganador] = logicaGanador;
            }
                
            victorias.Add(new RegistroVictoriasDto
            {
                Fecha = DateTime.Now,
                Ganador = ganador,
                Perdedor = perdedor
            });
        }

        private KeyValuePair<string, string>? ObtenerLogicaJugador(RegistroVictoriasDto registro)
        {
            if(logicas.TryGetValue(registro.Ganador, out string value))
            {
                return new KeyValuePair<string, string>(registro.Ganador, value);
            }

            return null;
        }

        private KeyValuePair<string, int> Agrup(IGrouping<string, RegistroVictoriasDto> ganador)
        {
            var victorias = ganador
                .Where(r => r.Perdedor != ganador.Key)
                .Select(r => r.Perdedor)
                .Distinct()
                .Count();
            var rta = new KeyValuePair<string, int>(ganador.Key, victorias);
            return rta;
        }
    }
}
