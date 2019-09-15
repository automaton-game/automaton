using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo.Tests
{
    [TestClass]
    public class RegistroPartidasTest
    {
        RegistroPartidas registro;

        Mock<ITareasTorneo> tareasTorneoMock;
        Mock<IRegistroPartidasDao> registroDaoMock;
        Mock<IRegistroJugadoresDao> registroJugadoresDaoMock;

        IDictionary<string, LogicaRobotDto> logicasRobots;
        IDictionary<string, RegistroJugadorDto> registroLogicas;

        IDictionary<string, RegistroPartidaEnCursoDto> partidasEnCurso;
        IList<RegistroJugadorDto> jugadoresInsertados;

        [TestInitialize]
        public void TestInitialize()
        {
            tareasTorneoMock = new Mock<ITareasTorneo>();
            registroDaoMock = new Mock<IRegistroPartidasDao>();
            registroJugadoresDaoMock = new Mock<IRegistroJugadoresDao>();

            registro = new RegistroPartidas(tareasTorneoMock.Object, registroDaoMock.Object, registroJugadoresDaoMock.Object);

            // Logicas jugadores
            logicasRobots = new Dictionary<string, LogicaRobotDto>();
            logicasRobots["A1"] = new LogicaRobotDto() { Usuario = "A", Logica = "Logica A1" };
            logicasRobots["A2"] = new LogicaRobotDto() { Usuario = "A", Logica = "Logica A2" };
            logicasRobots["A3"] = new LogicaRobotDto() { Usuario = "A", Logica = "Logica A3" };
            logicasRobots["B1"] = new LogicaRobotDto() { Usuario = "B", Logica = "Logica B1" };
            logicasRobots["C1"] = new LogicaRobotDto() { Usuario = "C", Logica = "Logica C1" };
            logicasRobots["D1"] = new LogicaRobotDto() { Usuario = "D", Logica = "Logica D1" };

            registroLogicas = new Dictionary<string, RegistroJugadorDto>();
            registroLogicas["A1"] = new RegistroJugadorDto() { Usuario = "A", Logica = "Logica A1" };
            registroLogicas["A2"] = new RegistroJugadorDto() { Usuario = "A", Logica = "Logica A2" };
            registroLogicas["A3"] = new RegistroJugadorDto() { Usuario = "A", Logica = "Logica A3" };
            registroLogicas["B1"] = new RegistroJugadorDto() { Usuario = "B", Logica = "Logica B1" };
            registroLogicas["C1"] = new RegistroJugadorDto() { Usuario = "C", Logica = "Logica C1" };
            registroLogicas["D1"] = new RegistroJugadorDto() { Usuario = "D", Logica = "Logica D1" };

            // Preparo un listado de posibles respuestas
            partidasEnCurso = new Dictionary<string, RegistroPartidaEnCursoDto>();

            // Configuro el mocks para que devuelva una partida resuelta segun cada caso
            foreach (var logicaRobotA in registroLogicas)
            {
                foreach (var logicaRobotB in registroLogicas)
                {
                    if (logicaRobotA.Key != logicaRobotB.Key)
                    {
                        SetupIniciarPartidaAsync(logicaRobotA, logicaRobotB);
                    }
                }
            }

            // Quito jugadores eliminados
            registroDaoMock
                .Setup(s => s.Delete(It.IsAny<int>()))
                .Callback<int>(id => {
                    var idEliminar = partidasEnCurso
                        .Where(p => p.Value.IdPartida == id)
                        .Select(p => p.Key)
                        .First();
                    partidasEnCurso.Remove(idEliminar);
                });

            // Devuelvo jugadores registrados
            jugadoresInsertados = new List<RegistroJugadorDto>();
            registroJugadoresDaoMock
                .Setup(s => s.Insert(It.Is<LogicaRobotDto>(l => logicasRobots.Values.Contains(l))))
                .Callback<LogicaRobotDto>(l => {
                    var key = logicasRobots.Where(r => r.Value == l).Select(r => r.Key).FirstOrDefault();
                    jugadoresInsertados.Add(registroLogicas[key]);
                })
                .Returns<LogicaRobotDto>(l =>
                {
                    var rta = jugadoresInsertados.Any(j => j.Usuario == l.Usuario);
                    return Task.FromResult(rta);
                });

            registroJugadoresDaoMock
                .Setup(s => s.GetAll())
                .ReturnsAsync(jugadoresInsertados.GroupBy(g => g.Usuario).Select(g => g.LastOrDefault()));
        }

        /// <summary>
        /// En este caso solo registro un jugador. No deberia hacerse nada.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RegistroSoloJugadorA()
        {
            // registro jugador A
            await registro.RegistrarRobotAsync(logicasRobots["A1"]);

            // verifico que no se hayan generado partidas
            Assert.AreEqual(0, partidasEnCurso.Count);

            // Verifico que no se haya intentado obtener el listado de partidas.
            registroDaoMock.Verify(v => v.GetAll(), Times.Never);

            // verifico que se haya registrado el jugador
            registroJugadoresDaoMock.Verify(v => v.Insert(logicasRobots["A1"]), Times.Once);
        }

        /// <summary>
        /// En este caso solo registro un jugador. No deberia hacerse nada.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RegistroSoloJugadorAConNuevaLogica()
        {
            await RegistroSoloJugadorA();

            // registro jugador A
            await registro.RegistrarRobotAsync(logicasRobots["A2"]);

            // verifico que no se hayan generado partidas
            Assert.AreEqual(0, partidasEnCurso.Count);

            // Verifico que no se haya intentado obtener el listado de partidas.
            registroDaoMock.Verify(v => v.GetAll(), Times.Never);

            // verifico que se haya registrado el jugador
            registroJugadoresDaoMock.Verify(v => v.Insert(logicasRobots["A2"]), Times.Once);
        }

        /// <summary>
        /// Registro 2 jugadores
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RegistroJugadoresAB()
        {
            //simulo jugador previo ya insertado
            await RegistroSoloJugadorA();

            // registro jugador B
            await registro.RegistrarRobotAsync(logicasRobots["B1"]);
            
            // verifico que se hayan generado 2 partidas
            Assert.IsTrue(partidasEnCurso.Keys.Contains("A1B1"), "No se registro la partida A1B1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("B1A1"), "No se registro la partida B1A1");

            // Verifico que no se haya intentado obtener el listado de partidas.
            registroDaoMock.Verify(v => v.GetAll(), Times.Never);

            // verifico que se haya registrado el jugador
            registroJugadoresDaoMock.Verify(v => v.Insert(logicasRobots["B1"]), Times.Once);
        }

        /// <summary>
        /// Registro 3 jugadores
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RegistroJugadoresABC()
        {
            await RegistroJugadoresAB();

            // registro jugadores
            await registro.RegistrarRobotAsync(logicasRobots["C1"]);

            // verifico que se hayan generado 6 partidas
            Assert.AreEqual(6, partidasEnCurso.Count, " 6 partidas en curso");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("C1A1"), "No se registro la partida C1A1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("C1B1"), "No se registro la partida C1B1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("B1C1"), "No se registro la partida B1C1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("A1C1"), "No se registro la partida A1C1");

            // Verifico que no se haya intentado obtener el listado de partidas.
            registroDaoMock.Verify(v => v.GetAll(), Times.Never);
        }

        /// Simulo varias request simulateas. 3 jugadores se incriben al mismo tiempo. 
        /// A medida que los jugadores se vayan inscribiendo, deberan generarse la cantidad de partidas correcta en cada momento.
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RegistroJugadoresABCSimultaneo()
        {
            var registroA = registro.RegistrarRobotAsync(logicasRobots["A1"]);
            var registroB = registro.RegistrarRobotAsync(logicasRobots["B1"]);
            var registroC = registro.RegistrarRobotAsync(logicasRobots["C1"]);

            // Espero a que finalicen las 3 request.
            await Task.WhenAll(registroA, registroB, registroC);

            // verifico que se hayan generado 6 partidas
            Assert.AreEqual(6, partidasEnCurso.Count, " 6 partidas en curso");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("B1A1"), "No se registro la partida B1A1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("A1B1"), "No se registro la partida A1B1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("C1A1"), "No se registro la partida C1A1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("C1B1"), "No se registro la partida C1B1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("B1C1"), "No se registro la partida B1C1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("A1C1"), "No se registro la partida A1C1");

            // Verifico que no se haya intentado obtener el listado de partidas.
            registroDaoMock.Verify(v => v.GetAll(), Times.Never);
        }

        /// Registro una nueva version de logica. Debe mantenerse la cantidad total de partidos
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task RegistroJugadoresA2BC()
        {

            // registro jugadores
            var registroA = registro.RegistrarRobotAsync(logicasRobots["A1"]);
            var registroB = registro.RegistrarRobotAsync(logicasRobots["B1"]);
            var registroA2 = registroA.ContinueWith(t => { registro.RegistrarRobotAsync(logicasRobots["A2"]).Wait(); });
            var registroC = registro.RegistrarRobotAsync(logicasRobots["C1"]);
            
            // Espero a que se completen las partidas
            await Task.WhenAll(registroA, registroA2, registroB, registroC);

            Assert.AreEqual(6, partidasEnCurso.Count(), "Deberían haber 6 partidas finalizadas");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("A2B1"), "No contiene A2B1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("A2C1"), "No contiene A2C1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("B1C1"), "No contiene B1C1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("C1B1"), "No contiene C1B1");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("B1A2"), "No contiene B1A2");
            Assert.IsTrue(partidasEnCurso.Keys.Contains("C1A2"), "No contiene C1A2");
        }

        private void SetupIniciarPartidaAsync(KeyValuePair<string, RegistroJugadorDto> jugadorA, KeyValuePair<string, RegistroJugadorDto> jugadorB)
        {
            var key = string.Concat(jugadorA.Key, jugadorB.Key);
            var partidaEnCurso = new RegistroPartidaEnCursoDto();

            tareasTorneoMock
                .Setup(s =>
                    s.IniciarPartida(It.Is<ICollection<LogicaRobotDto>>(c =>
                        c.Count == 2
                        && c.ElementAt(0) == jugadorA.Value
                        && c.ElementAt(1) == jugadorB.Value
                    )))
                .Callback(() => {
                    partidasEnCurso[key] = partidaEnCurso;
                    partidaEnCurso.IdPartida = partidasEnCurso.Keys.ToList().IndexOf(key);
                })
                .ReturnsAsync(partidaEnCurso);
        }
    }
}
