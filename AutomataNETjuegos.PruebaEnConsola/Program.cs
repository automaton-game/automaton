using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Helpers;
using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.Logica;
using AutomataNETjuegos.Robots;
using System;
using System.Collections.Generic;

namespace AutomataNETjuegos.PruebaEnConsola
{
    public class RobotUsuario : IRobot
    {
        public Tablero Tablero
        {
            get;
            set;
        }

        public AccionRobotDto GetAccionRobot()
        {
            var casillero = this.GetPosition(Tablero);
            if (casillero.Muralla == null && casillero.Robots.Count == 1)
            {
                return new AccionConstruirDto()
                { };
            }

            var direcciones = new List<DireccionEnum>();
            var direccion = GenerarDireccionAleatoria(direcciones);
            var movimiento = EvaluarMovimiento(casillero, direccion);
            while (movimiento == null)
            {
                direcciones.Add(direccion);
                if (direcciones.Count >= 4)
                {
                    return null;
                }

                direccion = GenerarDireccionAleatoria(direcciones);
                movimiento = EvaluarMovimiento(casillero, direccion);
            }

            return movimiento;
        }

        private AccionMoverDto EvaluarMovimiento(Casillero casillero, DireccionEnum direccion)
        {
            var relativo = casillero.BuscarRelativo(direccion);
            if (relativo != null)
            {
                if (relativo.Muralla == null || relativo.Muralla == this)
                {
                    return new AccionMoverDto()
                    { Direccion = direccion };
                }
            }

            return null;
        }

        private DireccionEnum GenerarDireccionAleatoria()
        {
            var random = new Random().Next(0, 4);
            return (DireccionEnum)random;
        }

        private DireccionEnum GenerarDireccionAleatoria(IList<DireccionEnum> evitar)
        {
            var obtenido = GenerarDireccionAleatoria();
            while (evitar.Contains(obtenido))
            {
                obtenido = GenerarDireccionAleatoria();
            }

            return obtenido;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var fabricaTablero = new FabricaTablero();
            var fabricaRobot = new FabricaRobotType();
            var robot1 = fabricaRobot.ObtenerRobot(typeof(RobotUsuario));
            var robot2 = fabricaRobot.ObtenerRobot(typeof(RobotDefensivo));

            var juego = new Juego2v2(fabricaTablero);

            juego.AgregarRobot(robot1);
            juego.AgregarRobot(robot2);

            
            var usuarioGanador = juego.ObtenerUsuarioGanador();
            var cont = 0;

            var motivo = juego.JugarTurno();
            while ( motivo == null)
            {
                cont++;
                motivo = juego.JugarTurno();

                if(cont > 1000)
                {
                    throw new Exception("Se excedió el limite de turnos");
                }
            }

            Console.WriteLine("Rondas {0}: motivo '{1}'", cont, motivo);
            Console.ReadLine();
        }
    }
}
