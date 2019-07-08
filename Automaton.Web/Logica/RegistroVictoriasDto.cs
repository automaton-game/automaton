using System;

namespace Automaton.Web.Logica
{
    public class RegistroVictoriasDto
    {
        public string Usuario { get; set; }

        public int Victorias { get; set; }

        public DateTime Fecha { get; set; }

        public string Logica { get; set; }
    }
}