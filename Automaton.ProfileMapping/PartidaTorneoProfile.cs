using Automaton.Contratos.Entorno;
using Automaton.Logica.Dtos;
using Automaton.Logica.Dtos.Model;
using Automaton.Logica.Dtos.Model.Torneo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Automaton.ProfileMapping
{
    public class PartidaTorneoProfile : AutoMapper.Profile
    {
        public PartidaTorneoProfile()
        {
            CreateMap<IEnumerable<IRegistroPartidaDto>, PartidosTorneoModel>()
                .ForMember(x => x.Partidos, y => y.MapFrom(x => x))
                ;

            CreateMap<IRegistroPartidaDto, PartidoTorneo>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.IdPartida));

            CreateMap<RegistroPartidaResueltaDto, PartidoTorneo>()
                .IncludeBase<IRegistroPartidaDto, PartidoTorneo>()
                .ForMember(x => x.Ganador, y => y.MapFrom(x => x.Ganador))
                .ForMember(x => x.PorcentajeProgreso, y => y.UseValue(100));

            CreateMap<RegistroPartidaEnCursoDto, PartidoTorneo>()
                .IncludeBase<IRegistroPartidaDto, PartidoTorneo>()
                .ForMember(x => x.Ganador, y => y.Ignore())
                .ForMember(x => x.PorcentajeProgreso, y => y.UseValue(new Random().Next(20,80)));

            CreateMap<PartidaResueltaDto, JuegoResponse>()
                .ForMember(x => x.Ganador, y => y.MapFrom(x => x.Ganador))
                .ForMember(x => x.MotivoDerrota, y => y.MapFrom(x => x.MotivoDerrota))
                .ForMember(x => x.Tableros, y => y.MapFrom(x => x.Tableros))
                ;

            CreateMap<TableroLogico, TableroDto>()
                .ForMember(x => x.Consola, y => y.Ignore())
                .ForMember(x => x.Filas, y => y.MapFrom(x => x.Filas))
                .ForMember(x => x.TurnoRobot, y => y.MapFrom(x => x.TurnoRobot))
                ;

            CreateMap<FilaTablero, FilaTableroDto>()
                .ForMember(x => x.NroFila, y => y.MapFrom(x => x.NroFila))
                .ForMember(x => x.Casilleros, y => y.MapFrom(x => x.Casilleros))
                ;

            CreateMap<Casillero, CasilleroDto>()
                .ForMember(x => x.NroFila, y => y.MapFrom(x => x.NroFila))
                .ForMember(x => x.NroColumna, y => y.MapFrom(x => x.NroColumna))
                .ForMember(x => x.Muralla, y => y.MapFrom(x => x.Muralla))
                ;
        }
    }
}
