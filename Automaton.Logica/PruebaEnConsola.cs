using Automaton.Logica.Robots;
using System;

namespace Automaton.Logica
{
    public class PruebaEnConsola
    {
        private readonly FabricaRobotType fabricaRobot;
        private readonly IJuego2v2 juego;

        public PruebaEnConsola()
        {
            var fabricaTablero = new FabricaTablero();
            fabricaRobot = new FabricaRobotType();
            juego = new Juego2v2(fabricaTablero);
        }

        public int Cont { private set; get; }

        public void AgregarRobot(Type type)
        {
            var robot1 = fabricaRobot.ObtenerRobot(type);
            juego.AgregarRobot(robot1);
        }

        public string Main()
        {
            var robot2 = fabricaRobot.ObtenerRobot(typeof(RobotDefensivo));
            juego.AgregarRobot(robot2);


            var usuarioGanador = juego.ObtenerUsuarioGanador();

            var motivo = juego.JugarTurno();
            while (motivo == null)
            {
                Cont++;
                motivo = juego.JugarTurno();

                if (Cont > 1000)
                {
                    throw new Exception("Se excedió el limite de turnos");
                }
            }

            return motivo;
        }
    }
}
