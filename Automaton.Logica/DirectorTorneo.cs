using Automaton.Logica.Dtos;
using Automaton.Logica.Factories;
using Automaton.Logica.Registro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public class DirectorTorneo : IDirectorTorneo
    {
        private readonly IFabricaRobotAsync fabricaRobot;
        private readonly IJuegoFactory juegoFactory;

        public DirectorTorneo(
            IFabricaRobotAsync fabricaRobot,
            IJuegoFactory juegoFactory)
        {
            this.fabricaRobot = fabricaRobot;
            this.juegoFactory = juegoFactory;
        }

        public PartidaResueltaDto Iniciar(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            var task = IniciarPartidaAsync(logicaRobotDtos);
            task.Wait();
            return task.Result;
        }

        public async Task<PartidaResueltaDto> IniciarPartidaAsync(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            var juego = juegoFactory.CreateJuego2V2();

            foreach (var logicaRobotDto in logicaRobotDtos)
            {
                var r = await fabricaRobot.ObtenerRobotAsync(logicaRobotDto.Logica);
                var tipo = r.GetType();
                juego.AgregarRobot(logicaRobotDto.Usuario, r);
            }

            var partida = GetPartidaResuelta(juego);
            partida.Jugadores = logicaRobotDtos.Select(j => j.Usuario).ToArray();

            return partida;
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