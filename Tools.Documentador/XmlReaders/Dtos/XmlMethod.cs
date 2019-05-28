using System.Collections.Generic;

namespace Tools.Documentador.Models
{
    public class XmlMethod : XmlMember
    {
        public ICollection<XmlMember> Params { get; set; }
    }
}
