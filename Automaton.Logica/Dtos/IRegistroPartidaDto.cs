namespace Automaton.Logica.Dtos
{
    public interface IRegistroPartidaDto : IPartidaDto
    {
        int IdPartida { get; set; }

        /// <summary>
        /// 0: Cancelado
        /// 50: En progreso
        /// 100: Completo
        /// </summary>
        short PorcentajeProgreso { get; set; }
    }
}
