using Automaton.Logica.Dtos;
using Automaton.Web.Models.Torneo;
using System.Collections.Generic;
using System.Linq;

namespace Automaton.Web.MappingProfiles
{
    public class PartidaTorneoProfile : AutoMapper.Profile
    {
        public PartidaTorneoProfile()
        {
            CreateMap<IEnumerable<IRegistroPartidaDto>, PartidosTorneoModel>()
                .ForMember(x => x.Partidos, y => y.MapFrom(x => x.ToList()))
                ;

            CreateMap<IRegistroPartidaDto, PartidoTorneo>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.IdPartida));
        }
    }
}
