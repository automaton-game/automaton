using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo.Tests
{
    [TestClass]
    public class RegistroJugadoresTest
    {
        RegistroPartidas registro;
        Mock<IDirectorTorneo> moq1;
        Mock<IRegistroNotificador> moq2;

        [TestInitialize]
        public void TestInitialize()
        {
            moq1 = new Mock<IDirectorTorneo>();
            moq2 = new Mock<IRegistroNotificador>();

            moq1.Setup(s => s.IniciarPartidaAsync(It.IsAny<ICollection<LogicaRobotDto>>())).ReturnsAsync(new PartidaResueltaDto(), TimeSpan.FromMilliseconds(4000));

            //registro = new RegistroPartidas(moq1.Object, moq2.Object);
        }

        [TestMethod]
        public async Task TestMethod1()
        {

            LogicaRobotDto Jugador1 = new LogicaRobotDto();
            Jugador1.Usuario = "Jug1";
            LogicaRobotDto Jugador2 = new LogicaRobotDto();
            Jugador1.Usuario = "Jug2";

            // Registra jugador
            await registro.RegistrarRobotAsync(Jugador1);
            await registro.RegistrarRobotAsync(Jugador2);


            // Verifico que aun no se haya generado la notificacion
            moq2.Verify(v => v.NotificarUltimasPartidas(null), Times.Never);

            // Espero a que finalice la partida
           

            // Verifico que se haya invocado la notificacion.
            moq2.Verify(v => v.NotificarUltimasPartidas(null), Times.Once);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var cantPartidas = registro.ObtenerUltimasPartidasAsync();

            Assert.AreEqual(0, cantPartidas);

            var logica = registro.ObtenerUltimasPartidasAsync("HN1");
        }
    }
}
