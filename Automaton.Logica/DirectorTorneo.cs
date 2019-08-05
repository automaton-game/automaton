using Automaton.Contratos.Entorno;
using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public class DirectorTorneo : IDirectorTorneo
    {
        private readonly IFabricaRobot fabricaRobot;

        public DirectorTorneo(IFabricaRobot fabricaRobot)
        {
            this.fabricaRobot = fabricaRobot;
        }

        public async Task<PartidaResueltaDto> Iniciar(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            IJuego2v2 juego = null;

            foreach (var logicaRobotDto in logicaRobotDtos)
            {
                var r = fabricaRobot.ObtenerRobot(logicaRobotDto.Logica);
                var tipo = r.GetType();
                juego.AgregarRobot(logicaRobotDto.Usuario, r);
            }

            // Obtengo resultado de la partida
            var tableros = GetTableros(juego).ToList();

            // Registro jugador ganador
            var usuarioGanador = juego.ObtenerUsuarioGanador();

            return new PartidaResueltaDto
            {
                Tableros = null,
                Ganador = usuarioGanador
                //MotivoDerrota = tableros.Last().Consola.Last()  TODO
            };
        }

        private IEnumerable<Tablero> GetTableros(IJuego2v2 juego)
        {
            {
                yield return juego.Tablero;
            }

            var turnoFinal = false;
            while (!turnoFinal)
            {
                var resultado = juego.JugarTurno();
                yield return juego.Tablero;
                turnoFinal = (resultado is TurnoFinalDto);
            }
        }
    }
}