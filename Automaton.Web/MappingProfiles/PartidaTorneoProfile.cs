using Automaton.Contratos.Entorno;
using Automaton.Logica.Dtos;
using Automaton.Web.Models;
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

            CreateMap<PartidaResueltaDto, JuegoResponse>()
                .ForMember(x => x.Ganador, y => y.MapFrom(x => x.Ganador))
                .ForMember(x => x.MotivoDerrota, y => y.MapFrom(x => x.MotivoDerrota))
                .ForMember(x => x.Tableros, y => y.MapFrom(x => x.Tableros))
                ;

            CreateMap<TableroLogico, TableroEntity>()
                .ForMember(x => x.Consola, y => y.Ignore())
                .ForMember(x => x.Filas, y => y.MapFrom(x => x.Filas))
                .ForMember(x => x.TurnoRobot, y => y.MapFrom(x => x.TurnoRobot))
                ;

            CreateMap<FilaTablero, FilaTableroEntity>()
                .ForMember(x => x.NroFila, y => y.MapFrom(x => x.NroFila))
                .ForMember(x => x.Casilleros, y => y.MapFrom(x => x.Casilleros))
                ;

            CreateMap<Casillero, CasilleroEntity>()
                .ForMember(x => x.NroFila, y => y.MapFrom(x => x.NroFila))
                .ForMember(x => x.NroColumna, y => y.MapFrom(x => x.NroColumna))
                .ForMember(x => x.Muralla, y => y.MapFrom(x => x.Muralla))
                ;
        }
    }
}
