using System;
using Automaton.Web.Models;

namespace Automaton.Web.MappingProfiles
{
    public class ErrorProfile : AutoMapper.Profile
    {
        public ErrorProfile()
        {
            CreateMap<Exception, ErrorModel>();

            CreateMap<string, ErrorModel>()
                .ForMember(x => x.Message, y => y.MapFrom(x => x));

            CreateMap<Exception, ErrorCompositorModel>()
                .ForMember(x => x.Errors, x => x.Ignore());
        }
    }
}
