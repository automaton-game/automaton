using Automaton.Logica.Dtos;
using Automaton.Logica.Factories;
using Automaton.Logica.Registro;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public class DirectorTorneo : IDirectorTorneo
    {
        private readonly IFabricaRobot fabricaRobot;
        private readonly IJuegoFactory juegoFactory;

        public DirectorTorneo(
            IFabricaRobot fabricaRobot,
            IJuegoFactory juegoFactory)
        {
            this.fabricaRobot = fabricaRobot;
            this.juegoFactory = juegoFactory;
        }

        public async Task<PartidaResueltaDto> Iniciar(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            var juego = juegoFactory.CreateJuego2V2();

            foreach (var logicaRobotDto in logicaRobotDtos)
            {
                var r = fabricaRobot.ObtenerRobot(logicaRobotDto.Logica);
                var tipo = r.GetType();
                juego.AgregarRobot(logicaRobotDto.Usuario, r);
            }

            var partida = Task.Run(() => GetPartidaResuelta(juego));
            return await partida;
        }

        private PartidaResueltaDto GetPartidaResuelta(IJuego2v2 juego)
        {
            // Obtengo resultado de la partida
            var tableros = new List<TableroLogico>();
            TurnoFinalDto turnoFinal = null;
            {
                tableros.Add(juego.Tablero);
                while (turnoFinal == null)
                {
                    var resultado = juego.JugarTurno();
                    tableros.Add(juego.Tablero);
                    turnoFinal = resultado as TurnoFinalDto;
                }
            }

            // Registro jugador ganador
            var usuarioGanador = juego.ObtenerUsuarioGanador();

            return new PartidaResueltaDto
            {
                Tableros = tableros,
                Ganador = usuarioGanador,
                MotivoDerrota = turnoFinal.Motivo
            };
        }
    }
}