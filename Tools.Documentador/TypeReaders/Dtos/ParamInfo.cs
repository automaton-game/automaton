namespace Tools.Documentador.Dtos
{
    public class ParamInfo : ItemMemberInfo
    {
        public override string ToString()
        {
            return $"{Type} {Name}";
        }
    }
}