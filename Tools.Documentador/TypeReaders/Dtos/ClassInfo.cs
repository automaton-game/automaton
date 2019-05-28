using System.Collections.Generic;

namespace Tools.Documentador.Dtos
{
    public class ClassInfo : ItemMemberInfo, IClassInfo
    {
        public ICollection<IMethodInfoDto> Items { get; set; }
    }
}
