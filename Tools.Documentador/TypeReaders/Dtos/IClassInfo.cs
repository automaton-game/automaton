using System.Collections.Generic;

namespace Tools.Documentador.Dtos
{
    public interface IClassInfo : IItemMemberInfo
    {
        ICollection<IMethodInfoDto> Methods { get; }

        ICollection<IItemMemberInfo> Properties { get; }
    }
}