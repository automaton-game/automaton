using System.Collections.Generic;

namespace Tools.Documentador.Dtos
{
    public interface IMethodInfoDto : IItemMemberInfo
    {
        ICollection<IItemMemberInfo> Params { get; }
    }
}