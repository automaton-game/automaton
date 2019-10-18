using AutoMapper;
using Automaton.Logica.Dtos;
using Automaton.Logica.Dtos.Model;
using Automaton.Logica.Excepciones;
using Automaton.Logica.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public class DirectorTorneo : IDirectorTorneo
    {
        private readonly IFabricaRobotAsync fabricaRobot;
        private readonly IJuegoFactory juegoFactory;
        private readonly IMapper mapper;

        public DirectorTorneo(
            IFabricaRobotAsync fabricaRobot,
            IJuegoFactory juegoFactory,
            IMapper mapper)
        {
            this.fabricaRobot = fabricaRobot;
            this.juegoFactory = juegoFactory;
            this.mapper = mapper;
        }

        public PartidaResueltaDto ResolverPartida(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            var task = ResolverPartidaAsync(logicaRobotDtos);
            task.Wait();
            return task.Result;
        }

        public async Task<PartidaResueltaDto> ResolverPartidaAsync(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            var juego = juegoFactory.CreateJuego2V2();

            foreach (var logicaRobotDto in logicaRobotDtos)
            {
                try
                {
                    var r = await fabricaRobot.ObtenerRobotAsync(logicaRobotDto.Logica);
                    var tipo = r.GetType();
                    juego.AgregarRobot(logicaRobotDto.Usuario, r);
                }
                catch (ExcepcionCompilacion ex)
                {
                    var errCompilacion = string.Join(Environment.NewLine, ex.ErroresCompilacion);
                    return new PartidaResueltaDto
                    {
                        Ganador = logicaRobotDtos.Where(f => f != logicaRobotDto).Select(s => s.Usuario).First(),
                        Jugadores = logicaRobotDtos.Select(j => j.Usuario).ToArray(),
                        MotivoDerrota = $" Error de compilación para el bot {logicaRobotDto.Usuario}: {errCompilacion}"
                    };
                }
            }

            var partida = GetPartidaResuelta(juego);
            partida.Jugadores = logicaRobotDtos.Select(j => j.Usuario).ToArray();

            return partida;
        }

        private PartidaResueltaDto GetPartidaResuelta(IJuego2v2 juego)
        {
            // Obtengo resultado de la partida
            var tableros = new List<TableroModel>();
            TurnoFinalDto turnoFinal = null;
            {
                {
                    var tableroModel = mapper.Map<TableroModel>(juego.Tablero);
                    tableros.Add(tableroModel);
                }

                while (turnoFinal == null)
                {
                    var resultado = juego.JugarTurno();
                    var tableroModel = mapper.Map<TableroModel>(juego.Tablero);
                    tableros.Add(tableroModel);
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