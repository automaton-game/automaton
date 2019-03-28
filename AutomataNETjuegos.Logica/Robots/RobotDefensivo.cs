using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Helpers;
using AutomataNETjuegos.Contratos.Robots;
using System;
using System.Collections.Generic;

namespace AutomataNETjuegos.Logica.Robots
{
    public class RobotDefensivo : IRobot
    {
        public Tablero Tablero { get; set; }

        public AccionRobotDto GetAccionRobot()
        {
            var casillero = this.GetPosition(Tablero);
            if (casillero.Muralla == null && casillero.Robots.Count == 1)
            {
                return new AccionConstruirDto() { };
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
                    return new AccionMoverDto() { Direccion = direccion };
                }
            }

            return null;
        }

        private DireccionEnum GenerarDireccionAleatoria()
        {
            var random = new Random().Next(0,4);
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
}
