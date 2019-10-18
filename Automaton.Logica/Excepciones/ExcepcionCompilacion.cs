using System;
using System.Collections.Generic;

namespace Automaton.Logica.Excepciones
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
