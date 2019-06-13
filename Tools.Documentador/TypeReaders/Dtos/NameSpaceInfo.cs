using System.Collections.Generic;
using Tools.Documentador.Dtos;

namespace Tools.Documentador.TypeReaders.Dtos
{
    public class NameSpaceInfo
    {
        public string Name { get; set; }

        public ICollection<NameSpaceInfo> NameSpaces { get; set; }

        public ICollection<IClassInfo> Classes { get; set; }
    }
}
