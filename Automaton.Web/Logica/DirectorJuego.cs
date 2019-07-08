using AutoMapper;
using Tablero = Automaton.Contratos.Entorno.Tablero;
using Automaton.Logica;
using Automaton.Logica.Robots;
using Automaton.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Automaton.Logica.Dtos;

namespace Automaton.Web.Logica
{
    public class DirectorJuego : IDirectorJuego
    {
        private readonly IJuego2v2 juego;
        private readonly IRegistroRobots registroRobots;
        private readonly IMapper mapper;
        private readonly IFabricaRobot fabricaRobot;

        public DirectorJuego(
            IJuego2v2 juego,
            IRegistroRobots registroRobots,
            IMapper mapper,
            IFabricaRobot fabricaRobot)
        {
            this.juego = juego;
            this.registroRobots = registroRobots;
            this.mapper = mapper;
            this.fabricaRobot = fabricaRobot;
        }

        public JuegoResponse Iniciar(string logicaRobot, string usuario)
        {
            var tipo = AgregarRobot(logicaRobot, usuario);
            var ultimoCampeon = registroRobots.ObtenerUltimoCampeon();
            if (ultimoCampeon != null && ultimoCampeon.Logica != null)
            {
                AgregarRobot(ultimoCampeon.Logica, ultimoCampeon.Usuario);
            }
            else
            {
                var jugador = typeof(RobotDefensivo);
                AgregarRobot(jugador);
            }

            var tableros = GetTableros(juego).ToArray();
            var usuarioGanador = juego.ObtenerUsuarioGanador();
            var logicaGanador = usuarioGanador == usuario ? logicaRobot : null;
            registroRobots.RegistrarVictoria(usuarioGanador, logicaGanador);

            return new JuegoResponse { Tableros = tableros, Ganador = usuarioGanador, MotivoDerrota = tableros.Last().Consola.Last() };
        }

        private void AgregarRobot(Type robotType)
        {
            var r = fabricaRobot.ObtenerRobot(robotType);
            juego.AgregarRobot(robotType.Name, r);
        }

        private Type AgregarRobot(string robotCode, string usuario)
        {
            var r = fabricaRobot.ObtenerRobot(robotCode);
            var tipo = r.GetType();
            juego.AgregarRobot(usuario, r);
            return tipo;
        }

        private IEnumerable<Models.Tablero> GetTableros(IJuego2v2 juego)
        {
            {
                var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
                yield return tablero;
            }

            var turnoFinal = false;
            while (!turnoFinal)
            {
                var resultado = juego.JugarTurno();
                var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
                mapper.Map(resultado, tablero);
                yield return tablero;
                turnoFinal = (resultado is TurnoFinalDto);
            }
        }
    }
}
