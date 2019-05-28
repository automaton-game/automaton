using System.Collections.Generic;

namespace Tools.Documentador.Dtos
{
    public interface IClassInfo : IItemMemberInfo
    {
        ICollection<IMethodInfoDto> Items { get; }
    }
}