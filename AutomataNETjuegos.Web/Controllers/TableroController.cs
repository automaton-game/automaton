using AutomataNETjuegos.Logica;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using AutomataNETjuegos.Robots;
using AutomataNETjuegos.Web.Models;
using Tablero = AutomataNETjuegos.Contratos.Entorno.Tablero;
using Microsoft.Extensions.Logging;
using System.Linq;
using AutomataNETjuegos.Web.Logica;
using AutomataNETjuegos.Contratos.Robots;
using System;

namespace AutomataNETjuegos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableroController : Controller
    {
        private readonly IJuego2v2 juego;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private readonly IRegistroRobots registroRobots;
        private readonly IRegistroJuegosManuales registroJuegosManuales;
        private readonly IFabricaRobot fabricaRobot;
        private string motivo;

        public TableroController(
            IJuego2v2 juego,
            IMapper mapper,
            ILogger<TableroController> logger,
            IRegistroRobots registroRobots,
            IRegistroJuegosManuales registroJuegosManuales,
            IFabricaRobot fabricaRobot)
        {
            this.juego = juego;
            this.mapper = mapper;
            this.logger = logger;
            this.registroRobots = registroRobots;
            this.registroJuegosManuales = registroJuegosManuales;
            this.fabricaRobot = fabricaRobot;
        }

        [HttpGet("[action]")]
        public JuegoManualResponse CrearTablero()
        {
            juego.AgregarRobot(new RobotManual());
            juego.AgregarRobot(new RobotManual());

            var id = registroJuegosManuales.Guardar(juego);

            var tableroModel = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
            var tableros = registroJuegosManuales.GuardarTablero(id, tableroModel);

            return new JuegoManualResponse { Jugadores = juego.Robots, jugadorTurnoActual = juego.ObtenerRobotTurnoActual().Usuario, Tableros = tableros, idTablero = id };
        }

        [HttpGet("[action]")]
        public JuegoManualResponse ObtenerTablero([FromQuery(Name = "idTablero")]string idTablero)
        {
            var juego = registroJuegosManuales.Obtener(idTablero);

            var tableros = registroJuegosManuales.ObtenerTableros(idTablero);

            return new JuegoManualResponse { Jugadores = juego.Robots, jugadorTurnoActual = juego.ObtenerRobotTurnoActual().Usuario, Tableros = tableros, idTablero = idTablero };
        }

        [HttpPost("[action]")]
        public JuegoManualResponse AccionarTablero(JuegoManualRequest juegoManualRequest)
        {
            var juego = registroJuegosManuales.Obtener(juegoManualRequest.IdTablero);
            var robot = juego.ObtenerRobotTurnoActual();
            var jugador = robot.Robot as RobotManual;

            if (robot.Usuario != juegoManualRequest.IdJugador)
            {
                throw new System.Exception("Jugador actual incorrecto");
            }

            switch (juegoManualRequest.AccionRobot)
            {
                case AccionRobot.Construir:
                    jugador.AccionRobot = new AccionConstruirDto();
                    break;
                case AccionRobot.Arriba:
                    jugador.AccionRobot = new AccionMoverDto { Direccion = DireccionEnum.Arriba };
                    break;
                case AccionRobot.Abajo:
                    jugador.AccionRobot = new AccionMoverDto { Direccion = DireccionEnum.Abajo };
                    break;
                case AccionRobot.Izquierda:
                    jugador.AccionRobot = new AccionMoverDto { Direccion = DireccionEnum.Izquierda };
                    break;
                case AccionRobot.Derecha:
                    jugador.AccionRobot = new AccionMoverDto { Direccion = DireccionEnum.Derecha };
                    break;
            }

            
            string ganador = null;
            if (!JugarTurno(juego))
            {
                ganador = juego.ObtenerUsuarioGanador();
            }

            var tableroModel = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
            var tableros = registroJuegosManuales.GuardarTablero(juegoManualRequest.IdTablero, tableroModel);

            return new JuegoManualResponse { Jugadores = juego.Robots, jugadorTurnoActual = juego.ObtenerRobotTurnoActual().Usuario, Tableros = tableros, idTablero = juegoManualRequest.IdTablero, Ganador = ganador, MotivoDerrota = motivo};
        }

        [HttpPost("[action]")]
        public JuegoResponse GetTablero(TableroRequest tableroRequest)
        {
            var tipo = AgregarRobot(tableroRequest.LogicaRobot);
            registroRobots.Registrar(tipo.Name, tableroRequest.LogicaRobot);
            
            var ultimoCampeon = registroRobots.ObtenerUltimoCampeon();
            if (ultimoCampeon != null)
            {
                AgregarRobot(ultimoCampeon);
            }
            else
            {
                var jugador = typeof(RobotDefensivo);
                AgregarRobot(jugador);
            }

            var tableros = GetTableros(juego).ToArray();
            var usuarioGanador = juego.ObtenerUsuarioGanador();
            registroRobots.RegistrarVictoria(usuarioGanador);

            return new JuegoResponse { Tableros = tableros, Ganador = usuarioGanador, MotivoDerrota = this.motivo };
        }

        private IEnumerable<Models.Tablero> GetTableros(IJuego2v2 juego)
        {
            {
                var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
                yield return tablero;
            }
            
            while (JugarTurno(juego))
            {
                var tablero = mapper.Map<Tablero, Models.Tablero>(juego.Tablero);
                yield return tablero;
            }
        }

        private void AgregarRobot(Type robotType)
        {
            var r = fabricaRobot.ObtenerRobot(robotType);
            juego.AgregarRobot(r);
        }

        private Type AgregarRobot(string robotCode)
        {
            var r = fabricaRobot.ObtenerRobot(robotCode);
            var tipo = r.GetType();
            juego.AgregarRobot(r);
            return tipo;
        }

        private bool JugarTurno(IJuego2v2 juego)
        {
            var motivo = juego.JugarTurno();
            this.motivo = motivo;
            return motivo == null;
        }
    }
}