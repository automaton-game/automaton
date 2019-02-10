using AutomataNETjuegos.Compilador.Excepciones;
using AutomataNETjuegos.Web.Models;
using System;

namespace AutomataNETjuegos.Web.MappingProfiles
{
    public class ErrorProfile : AutoMapper.Profile
    {
        public ErrorProfile()
        {
            CreateMap<Exception, ErrorModel>()
                .ForMember(x => x.Name, y => y.MapFrom(x => x.GetType().Name));

            CreateMap<string, ErrorModel>()
                .ForMember(x => x.Message, y => y.MapFrom(x => x))
                .ForMember(x => x.Name, y => y.MapFrom(x => x.GetType().Name));

            CreateMap<ExcepcionCompilacion, ErrorModel>()
                .ForMember(x => x.Errores, y => y.MapFrom(x => x.ErroresCompilacion))
                .ForMember(x => x.Name, y => y.MapFrom(x => x.GetType().Name));
        }
    }
}
