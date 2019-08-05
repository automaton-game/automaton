namespace Automaton.Logica.Registro
{
    public interface IRegistroRobots
    {
        void RegistrarRobot(string usuario, string logica);

        string ObtenerLogicaRobot(string usuario);
    }
}
