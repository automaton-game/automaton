namespace Automaton.Contratos.Robots
{
    /// <summary>
    /// Accion de mover
    /// </summary>
    public class AccionMoverDto : AccionRobotDto
    {
        /// <summary>
        /// Direccion en la cual se va a mover el robot
        /// </summary>
        public DireccionEnum Direccion { get; set; }
    }
}
