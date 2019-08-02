using Automaton.Logica.Registro;
using System;
using Xunit;

namespace Automaton.Logica.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var registro = new RegistroRobots();
            registro.RegistrarVictoria("RobotDefensivo", "HN", null);
            registro.RegistrarVictoria("RobotDefensivo", "HN", null);
            registro.RegistrarVictoria("RobotDefensivo", "OTRO", null);
            registro.RegistrarVictoria("HN", "RobotDefensivo", "LOGICA_HN");
            registro.RegistrarVictoria("HN", "OTRO", null);
            registro.RegistrarVictoria("HN", "OTRO", null);
            registro.RegistrarVictoria("OTRO", "HN", "LOGICA_OTRO");
            registro.RegistrarVictoria("OTRO", "HN", null);
            registro.RegistrarVictoria("HN", "OTRO", "LOGICA_HN1");
            registro.RegistrarVictoria("OTRO", "HN", "LOGICA_OTRO1");

            var camp = registro.ObtenerLogicaCampeon();
            Assert.Equal("LOGICA_OTRO1", camp.Value.Value);

            var resumen = registro.ObtenerResumen();
            Assert.Equal(2, resumen["RobotDefensivo"]);
            Assert.Equal(2, resumen["HN"]);
        }
    }
}
