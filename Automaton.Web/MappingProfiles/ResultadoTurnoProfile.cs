using Automaton.Logica.Dtos;
using Automaton.Web.Models;

namespace Automaton.Web.MappingProfiles
{
    public class ResultadoTurnoProfile : AutoMapper.Profile
    {
        public ResultadoTurnoProfile()
        {
            CreateMap<TurnoFinalDto, Tablero>()
                .ForMember(x => x.Filas, y => y.Ignore())
                .ForMember(x => x.Consola, y => y.MapFrom(x => new[] { x.Motivo }));

            CreateMap<TurnoRobotDto, Tablero>()
                .ForMember(x => x.Filas, y => y.Ignore())
                .ForMember(x => x.Consola, y => y.MapFrom(x => x.Consola));
        }
    }
}
