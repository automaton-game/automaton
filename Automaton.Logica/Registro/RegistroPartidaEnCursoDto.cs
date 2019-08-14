namespace Automaton.Logica.Registro
{
    public class RegistroPartidaEnCursoDto : PartidaDto, IRegistroPartidaDto
    {
        public int IdPartida { get; set; }

        public short PorcentajeProgreso { get; set; }
    }
}
