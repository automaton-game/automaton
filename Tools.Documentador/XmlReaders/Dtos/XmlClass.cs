using System.Collections.Generic;

namespace Tools.Documentador.Models
{
    public class XmlClass : XmlMember
    {
        public ICollection<XmlMethod> Methods { get; set; } 
    }
}
