using System.Collections.Generic;

namespace Tools.Documentador.Dtos
{
    public class ClassSummaryMember<TClassInfo> : SummaryMember<TClassInfo>, IClassInfo where TClassInfo : IClassInfo
    {
        public ICollection<IMethodInfoDto> Methods => this.ItemMemberInfo.Methods;

        public ICollection<IItemMemberInfo> Properties => this.ItemMemberInfo.Properties;

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}
