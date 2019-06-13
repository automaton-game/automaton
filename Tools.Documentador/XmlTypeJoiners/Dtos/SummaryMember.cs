using Tools.Documentador.Models;

namespace Tools.Documentador.Dtos
{
    public class SummaryMember<TItemMemberInfo> : IItemMemberInfo, IXmlSummary where TItemMemberInfo : IItemMemberInfo
    {
        public TItemMemberInfo ItemMemberInfo { get; set; }

        public string Summary { get; set; }

        public string Name => ItemMemberInfo.Name;

        public string Type => ItemMemberInfo.Type;

        public string Namespace => ItemMemberInfo.Namespace;
    }
}
