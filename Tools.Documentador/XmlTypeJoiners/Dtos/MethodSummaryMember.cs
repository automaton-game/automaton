using System.Collections.Generic;
using Tools.Documentador.Dtos;

namespace Tools.Documentador.XmlTypeJoiners.Dtos
{
    public class MethodSummaryMember<TMethodInfo> : SummaryMember<TMethodInfo>, IMethodInfoDto where TMethodInfo : IMethodInfoDto
    {
        public ICollection<IItemMemberInfo> Params => ItemMemberInfo.Params;

        public override string ToString()
        {
            return ItemMemberInfo.ToString();
        }
    }
}
