﻿using System.Collections.Generic;
using Automaton.Contratos.Entorno;
using Automaton.Logica.Dtos;

namespace Automaton.Logica
{
    public interface IJuegoTurno
    {
        void Configurar(TableroDto tablero, RobotJuegoDto robotJuego, IEnumerable<RobotJuegoDto> accionesRobot);
        ResultadoTurnoDto JugarTurno();
    }
}