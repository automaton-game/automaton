using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Helpers;
using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.Logica.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomataNETjuegos.Logica
{
    public class Juego2v2 : IJuego2v2
    {
        private readonly IFabricaTablero fabricaTablero;
        private readonly IFabricaRobot fabricaRobot;

        private ICollection<IRobot> robots => accionesRobot.Select(s => s.Robot).ToArray();
        private IList<RobotJuegoDto> accionesRobot = new List<RobotJuegoDto>();

        public Juego2v2(
            IFabricaTablero fabricaTablero,
            IFabricaRobot fabricaRobot
            )
        {
            this.fabricaTablero = fabricaTablero;
            this.fabricaRobot = fabricaRobot;
        }

        public ICollection<string> Robots => accionesRobot.Select(r => r.Usuario).ToArray();

        public Tablero Tablero { get; private set; }

        public void AgregarRobot(Type robotType)
        {
            var r = fabricaRobot.ObtenerRobot(robotType);
            this.AgregarRobot(r.GetType().Name, r);
        }

        public Type AgregarRobot(string robotCode)
        {
            var r = fabricaRobot.ObtenerRobot(robotCode);
            var tipo = r.GetType();
            this.AgregarRobot(tipo.Name, r);
            return tipo;
        }

        public void AgregarRobot(IRobot robot)
        {
            var hash = robot.GetHashCode().ToString();
            this.AgregarRobot(hash, robot);
        }

        private void AgregarRobot(string usuario, IRobot robot)
        {
            this.accionesRobot.Add(new RobotJuegoDto { Usuario = usuario, Robot = robot });

            if (this.Tablero == null)
            {
                this.Tablero = fabricaTablero.Crear();
            }

            robot.Tablero = this.Tablero;

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

        public string JugarTurno()
        {
            var robotJuego = ObtenerRobotTurnoActual();
            try
            {
                var accion = EjecutarAccionRobot(robotJuego);
                this.accionesRobot.First(f => f.Robot == robotJuego.Robot).Acciones.Add(accion);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
            return null;
        }

        public RobotJuegoDto ObtenerRobotTurnoActual()
        {
            return this.accionesRobot.OrderBy(d => d.Acciones.Count).First();
        }

        public string ObtenerUsuarioGanador()
        {
            var perdedor = ObtenerRobotTurnoActual();
            return this.accionesRobot.Except(new[] { perdedor }).First().Usuario;
        }

        private AccionRobotDto EjecutarAccionRobot(RobotJuegoDto robotJuego)
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
                    var jugadorGanador = accionesRobot.Where(f => f.Robot == maxCantidadMurallas.Key).Select(s => s.Usuario).First();
                    throw new Exception($"El jugador {jugadorGanador} ya completo la mayoría de casilleros ({maxCantidadMurallas.Value})");
                }
            }

            // Valido que el robor devuelva alguna acción
            var accion = robot.GetAccionRobot();
            if (accion == null)
            {
                throw new Exception("El robot no devolvió ninguna accion");
            }

            // Valido que haya construido dentro de las ultimas aciones
            var movimientosSinConstruccion = robotJuego.Acciones.Reverse<AccionRobotDto>().TakeWhile(a => a is AccionMoverDto).Count();
            if (movimientosSinConstruccion > Tablero.Filas.Count * 2)
            {
                throw new Exception("Se excedió la cantidad maxima de movimientos sin construir");
            }

            var accionMover = accion as AccionMoverDto;
            if (accionMover != null)
            {
                var direccion = accionMover.Direccion;
                var casilleroActual = ObtenerPosicion(robot);
                var nuevoCasillero = Desplazar(casilleroActual, direccion, robot);
                nuevoCasillero.AgregarRobot(robot);
                casilleroActual.QuitarRobot(robot);

            }

            var accionMurralla = accion as AccionConstruirDto;
            if (accionMurralla != null)
            {
                var casilleroActual = ObtenerPosicion(robot);
                if (casilleroActual.Robots.Count != 1)
                {
                    throw new Exception("No es posible construir cuando hay más de un robot en el mismo casillero.");
                }

                casilleroActual.Muralla = robot;
            }

            return accion;
        }

        private Casillero ObtenerPosicion(IRobot robot)
        {
            return this.Tablero.GetPosition(robot);
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
