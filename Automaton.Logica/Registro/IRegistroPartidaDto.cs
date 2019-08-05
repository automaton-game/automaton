namespace Automaton.Logica.Registro
{
    public interface IRegistroPartidaDto : IPartidaDto
    {
        int IdPartida { get; set; }

        short PorcentajeProgreso { get; set; }
    }
}
