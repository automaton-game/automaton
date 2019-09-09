using AutoMapper;
using Automaton.Logica.Dtos;
using Automaton.Logica.Dtos.Model.Torneo;
using Automaton.ProfileMapping;
using Xunit;

namespace Automaton.Logica.Tests
{
    public class MapperTest
    {
        [Fact]
        public void TableroMappingTest()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<PartidaTorneoProfile>();

            });

            var mapper = config.CreateMapper();

            var registroPartidaResueltaDto = new RegistroPartidaResueltaDto()
            {
                Ganador = "ganador1",
                IdPartida = 5,
                Jugadores = new[] { "jugador1" , "jugador2" }
            };
            var mapped = mapper.Map<PartidoTorneo>(registroPartidaResueltaDto);

            Assert.NotNull(mapped);
        }

        [Fact]
        public void TableroMapping2()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<PartidaTorneoProfile>();

            });

            var mapper = config.CreateMapper();

            var registroPartidaResueltaDto = new RegistroPartidaEnCursoDto()
            {
                IdPartida = 5,
                Jugadores = new[] { "jugador1", "jugador2" }
            };
            var mapped = mapper.Map<PartidoTorneo>(registroPartidaResueltaDto);

            Assert.NotNull(mapped);
        }

        [Fact]
        public void TableroMapping3()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<PartidaTorneoProfile>();

            });

            var mapper = config.CreateMapper();

            var registroPartidaEnCursoDto = new RegistroPartidaEnCursoDto()
            {
                IdPartida = 5,
                Jugadores = new[] { "jugador1", "jugador2" }
            };
            var registroPartidaResueltaDto = new RegistroPartidaResueltaDto()
            {
                Ganador = "ganador1",
                IdPartida = 5,
                Jugadores = new[] { "jugador1", "jugador2" }
            };


            var mapped = mapper.Map<PartidosTorneoModel>(new IRegistroPartidaDto[] { registroPartidaEnCursoDto, registroPartidaResueltaDto});

            Assert.NotNull(mapped);
        }
    }
}
