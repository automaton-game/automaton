using AutomataNETjuegos.Contratos.Entorno;
using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.Logica.Dtos;
using System;
using System.Collections.Generic;

namespace AutomataNETjuegos.Logica
{
    public interface IJuego2v2
    {
        Tablero Tablero { get; }

        ICollection<string> Robots { get; }

        void AgregarRobot(IRobot robot);

        string JugarTurno();

        string ObtenerUsuarioGanador();

        RobotJuegoDto ObtenerRobotTurnoActual();
    }
}