using Automaton.Compilador.Excepciones;
using Automaton.Web.Models;
using System;

namespace Automaton.Web.MappingProfiles
{
    public class ErrorProfile : AutoMapper.Profile
    {
        public ErrorProfile()
        {
            CreateMap<Exception, ErrorModel>();

            CreateMap<string, ErrorModel>()
                .ForMember(x => x.Message, y => y.MapFrom(x => x));
        }
    }
}
