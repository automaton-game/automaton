using System;
using System.Collections.Generic;
using System.Linq;
using Automaton.Contratos.Entorno;
using Automaton.Contratos.Helpers;
using Automaton.Contratos.Robots;
using Automaton.Logica.Helpers;

public class RobotUsuario : ARobot
{
    public override AccionRobotDto GetAccionRobot(IConsole console)
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

public class Program
{
    public static void Main(string[] args)
    {
        var usuario1 = new RobotUsuario();
        var usuario2 = new RobotUsuario();
        usuario1.Tablero = Crear(usuario1);


        usuario1.Tablero.Filas.First().Casilleros.First().AgregarRobot(usuario1);
        usuario1.Tablero.Filas.Last().Casilleros.Last().AgregarRobot(usuario2);

        var accion = usuario1.GetAccionRobot(null);

        System.Console.WriteLine($"El primer movimiento es de tipo {accion.GetType().Name }");
        System.Console.ReadLine();
    }

    private static Tablero Crear(IRobot robot)
    {
         const int filas = 5;
        const int columnas = 5;

        var tablero = new Tablero();
        tablero.Filas = Enumerable.Range(1, filas).Select(f => {
            var fila = new FilaTablero
            {
                NroFila = f,
                Tablero = tablero
            };

            fila.Casilleros = Enumerable.Range(1, columnas).Select(c => new Casillero { Fila = fila, NroFila = f, NroColumna = c }).ToArray();
            return fila;
        }).ToArray();


        

        return tablero;
    }
}
