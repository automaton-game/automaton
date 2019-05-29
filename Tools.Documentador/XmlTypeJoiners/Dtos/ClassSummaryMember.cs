using System.Collections.Generic;
using Tools.Documentador.Models;

namespace Tools.Documentador.Dtos
{
    public class ClassSummaryMember<TClassInfo> : SummaryMember<TClassInfo>, IClassInfo where TClassInfo : IClassInfo
    {
        public ICollection<IMethodInfoDto> Items => this.ItemMemberInfo.Items;

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}
