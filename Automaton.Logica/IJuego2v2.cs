using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;
using Automaton.Logica.Dtos;
using System;
using System.Collections.Generic;

namespace Automaton.Logica
{
    public interface IJuego2v2
    {
        Tablero Tablero { get; }

        ICollection<string> Robots { get; }

        void AgregarRobot(IRobot robot);

        ResultadoTurnoDto JugarTurno();

        string ObtenerUsuarioGanador();

        RobotJuegoDto ObtenerRobotTurnoActual();
    }
}