namespace Automaton.Logica.Registro
{
    public class RegistroPartidaResueltaDto : PartidaResueltaDto, IRegistroPartidaDto
    {
        public int IdPartida { get; set; }

        public short PorcentajeProgreso { get; set; }
    }
}
