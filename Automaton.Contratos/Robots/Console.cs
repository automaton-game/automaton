using System;
using System.Collections.Generic;

namespace Automaton.Contratos.Robots
{
    public class Console : IConsole
    {
        public IList<string> Logs { get; private set; } = new List<string>();

        public void WriteLine(string format, params object[] args)
        {
            var limite = 50;
            if(Logs.Count == limite)
            {
                Logs.Add($"Se excedió el limite de {limite} operaciones de Consola.");
            }
            else if(Logs.Count < limite)
            {
                Logs.Add(string.Format(format, args));
            }
        }
    }
}
