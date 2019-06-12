namespace Tools.Documentador.Dtos
{
    public class ItemMemberInfo : IItemMemberInfo
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Type}";
        }
    }
}
