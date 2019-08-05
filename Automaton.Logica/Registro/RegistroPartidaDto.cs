using Automaton.Contratos.Entorno;
using System.Collections.Generic;

namespace Automaton.Logica.Registro
{
    public class RegistroPartidaDto
    {
        public int IdPartida { get; set; }

        public IList<string> Jugadores { get; set; }

        public string Ganador { get; set; }

        public short PorcentajeProgreso { get; set; }
    }

    public class RegistroPartidaCompletaDto : RegistroPartidaDto
    {
        public ICollection<Tablero> Tableros { get; set; }

        public string MotivoDerrota { get; set; }
    }
}
