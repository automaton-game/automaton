using Automaton.Contratos.Robots;
using Automaton.Contratos.Entorno;
using Automaton.Logica.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using Automaton.Contratos.Helpers;
using Automaton.Logica.Robots;

namespace Automaton.Logica
{
    public class JuegoTurno : IJuegoTurno
    {
        private readonly IFabricaTablero fabricaTablero;

        public JuegoTurno(IFabricaTablero fabricaTablero)
        {
            this.fabricaTablero = fabricaTablero;
        }

        public Tablero Tablero { get; private set; }

        public RobotJuegoDto RobotJuego { get; private set; }

        public IEnumerable<RobotJuegoDto> AccionesRobot { get; private set; }

        public void Configurar(Tablero tablero, RobotJuegoDto robotJuego, IEnumerable<RobotJuegoDto> accionesRobot)
        {
            // Escribo una copia del tablero por seguridad:
            this.Tablero = this.fabricaTablero.Clone(tablero);

            this.RobotJuego = robotJuego;
            this.AccionesRobot = accionesRobot;
        }

        public ResultadoTurnoDto JugarTurno()
        {
            try
            {
                RobotJuego.Robot.Tablero = this.Tablero;

                var console = new RobotConsole();
                var accion = EjecutarAccionRobot(RobotJuego, console);
                return new TurnoRobotDto { Accion = accion, Consola = console.Logs };
            }
            catch (Exception ex)
            {
                return new TurnoFinalDto { Motivo = ex.Message };
            }
        }

        private AccionRobotDto EjecutarAccionRobot(RobotJuegoDto robotJuego, IConsole console)
        {
            var robot = robotJuego.Robot;
            // Valido que el robot enemigo no tenga mayoria de casilleros
            var murallas = Tablero.Filas.SelectMany(s => s.Casilleros.Select(c => c.Muralla)).Where(m => m != null);
            var gruposDeMurallas = murallas.GroupBy(m => m);
            var cantidadPorGrupo = gruposDeMurallas.ToDictionary(g => g.Key, g => g.Count());
            if (cantidadPorGrupo.Any())
            {
                var maxCantidadMurallas = cantidadPorGrupo.OrderByDescending(o => o.Value).First();
                var maxSlotsDisponibles = Tablero.Filas.Count * Tablero.Filas.Count;
                var cantidadRobots = gruposDeMurallas.Count();
                var cantidadNecesariaParaGanar = (maxSlotsDisponibles / cantidadRobots) + 1;
                if (maxCantidadMurallas.Value >= cantidadNecesariaParaGanar)
                {
                    var jugadorGanador = AccionesRobot.Where(f => f.Robot == maxCantidadMurallas.Key).Select(s => s.Usuario).First();
                    throw new Exception($"El jugador {jugadorGanador} ya completo la mayoría de casilleros ({maxCantidadMurallas.Value})");
                }
            }

            // Valido que el robor devuelva alguna acción
            var accion = robot.GetAccionRobot(console);
            if (accion == null)
            {
                throw new Exception("El robot no devolvió ninguna accion");
            }

            // Valido que haya construido dentro de las ultimas aciones
            var movimientosSinConstruccion = robotJuego.Turnos.Reverse<ResultadoTurnoDto>().Where(x => x is TurnoRobotDto).Cast<TurnoRobotDto>().Select(t => t.Accion).TakeWhile(a => a is AccionMoverDto).Count();
            if (movimientosSinConstruccion > Tablero.Filas.Count * 2)
            {
                throw new Exception("Se excedió la cantidad maxima de movimientos sin construir");
            }

            var accionMover = accion as AccionMoverDto;
            if (accionMover != null)
            {
                var direccion = accionMover.Direccion;
                var casilleroActual = robot.GetPosition();
                var nuevoCasillero = Desplazar(casilleroActual, direccion, robot);
                nuevoCasillero.AgregarRobot(robot);
                casilleroActual.QuitarRobot(robot);

            }

            var accionMurralla = accion as AccionConstruirDto;
            if (accionMurralla != null)
            {
                var casilleroActual = robot.GetPosition();
                if (casilleroActual.Robots.Count != 1)
                {
                    throw new Exception("No es posible construir cuando hay más de un robot en el mismo casillero.");
                }

                casilleroActual.Muralla = robot;
            }

            return accion;
        }



        private Casillero Desplazar(Casillero casilleroOrigen, DireccionEnum movimiento, IRobot robot)
        {
            var posFila = this.Tablero.Filas.IndexOf(casilleroOrigen.Fila);
            var posColumna = casilleroOrigen.Fila.Casilleros.IndexOf(casilleroOrigen);

            switch (movimiento)
            {
                case DireccionEnum.Arriba:
                    posFila--;
                    break;
                case DireccionEnum.Abajo:
                    posFila++;
                    break;
                case DireccionEnum.Izquierda:
                    posColumna--;
                    break;
                case DireccionEnum.Derecha:
                    posColumna++;
                    break;
                default:
                    break;
            }

            var fila = this.Tablero.Filas.ElementAtOrDefault(posFila);
            if (fila == null)
            {
                throw new Exception("Movimiento fuera del tablero!");
            }

            var casillero = fila.Casilleros.ElementAtOrDefault(posColumna);
            if (casillero == null)
            {
                throw new Exception("Movimiento fuera del tablero!");
            }

            if (casillero.Muralla != null && casillero.Muralla != robot)
            {
                throw new Exception(string.Format("Hay una muralla ocupando la posicion {0}, {1}", casillero.NroColumna, casillero.NroFila));
            }

            return casillero;
        }
    }
}
