using Automaton.Contratos.Entorno;
using Automaton.Contratos.Helpers;
using Automaton.Contratos.Robots;
using Automaton.Logica.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automaton.Logica
{
    public class Juego2v2 : IJuego2v2
    {
        private readonly IFabricaTablero fabricaTablero;
        private readonly IJuegoTurno juegoTurno;

        private ICollection<IRobot> robots => robotsJuegoDto.Select(s => s.Robot).ToArray();
        private IList<RobotJuegoDto> robotsJuegoDto = new List<RobotJuegoDto>();

        public Juego2v2(
            IFabricaTablero fabricaTablero,
            IJuegoTurno juegoTurno
            )
        {
            this.fabricaTablero = fabricaTablero;
            this.juegoTurno = juegoTurno;
        }

        public ICollection<string> Robots => robotsJuegoDto.Select(r => r.Usuario).ToArray();

        public Tablero Tablero { get; private set; }

        public void AgregarRobot(IRobot robot)
        {
            var typeName = robot.GetType().Name;
            this.AgregarRobot(typeName, robot);
        }

        public ResultadoTurnoDto JugarTurno()
        {
            var robotJuego = ObtenerRobotTurnoActual();
            this.juegoTurno.Configurar(this.Tablero, robotJuego, this.robotsJuegoDto);
            var turno = this.juegoTurno.JugarTurno();
            this.robotsJuegoDto.First(f => f.Robot == robotJuego.Robot).Turnos.Add(turno);
            
            return turno;
        }

        public RobotJuegoDto ObtenerRobotTurnoActual()
        {
            return this.robotsJuegoDto.OrderBy(d => d.Turnos.Count).First();
        }

        public string ObtenerUsuarioGanador()
        {
            var perdedor = ObtenerRobotTurnoActual();
            return this.robotsJuegoDto.Except(new[] { perdedor }).First().Usuario;
        }

        private void AgregarRobot(string usuario, IRobot robot)
        {
            this.robotsJuegoDto.Add(new RobotJuegoDto { Usuario = usuario, Robot = robot });

            if (this.Tablero == null)
            {
                this.Tablero = fabricaTablero.Crear();
            }

            switch (this.robots.Count)
            {
                case 1:
                    this.Tablero.Filas.First().Casilleros.First().AgregarRobot(robot);
                    break;

                case 2:
                    this.Tablero.Filas.Last().Casilleros.Last().AgregarRobot(robot);
                    break;

                case 3:
                    this.Tablero.Filas.Last().Casilleros.First().AgregarRobot(robot);
                    break;

                case 4:
                    this.Tablero.Filas.First().Casilleros.Last().AgregarRobot(robot);
                    break;
            }
        }
    }
}
