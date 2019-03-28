using System;
using System.Collections.Generic;

namespace Automaton.Compilador.Excepciones
{
    public class ExcepcionCompilacion : Exception
    {
        public ExcepcionCompilacion()
        {
            HResult = 23245688;
        }

        public IList<string> ErroresCompilacion { get; set; }
    }
}
