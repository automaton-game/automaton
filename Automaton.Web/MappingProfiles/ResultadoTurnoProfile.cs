using Automaton.Logica.Dtos;
using Automaton.Logica.Dtos.Model;
using Automaton.Web.Models;

namespace Automaton.Web.MappingProfiles
{
    public class ResultadoTurnoProfile : AutoMapper.Profile
    {
        public ResultadoTurnoProfile()
        {
            CreateMap<TurnoFinalDto, TableroModel>()
                .ForMember(x => x.Filas, y => y.Ignore())
                .ForMember(x => x.Consola, y => y.MapFrom(x => new[] { x.Motivo }));

            CreateMap<TurnoRobotDto, TableroModel>()
                .ForMember(x => x.Filas, y => y.Ignore())
                .ForMember(x => x.Consola, y => y.MapFrom(x => x.Consola));
        }
    }
}
