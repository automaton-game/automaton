using System.Collections.Generic;

namespace Tools.Documentador.Dtos
{
    public class ClassInfo : ItemMemberInfo, IClassInfo
    {
        public ICollection<IMethodInfoDto> Methods { get; set; }

        public ICollection<IItemMemberInfo> Properties { get; set; }

        public override string ToString()
        {
            return $"{Type}";
        }
    }
}
