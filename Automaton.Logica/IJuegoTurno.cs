using System.Collections.Generic;
using Automaton.Logica.Dtos;

namespace Automaton.Logica
{
    public interface IJuegoTurno
    {
        void Configurar(TableroLogico tablero, RobotJuegoDto robotJuego, IEnumerable<RobotJuegoDto> accionesRobot);
        ResultadoTurnoDto JugarTurno();
    }
}