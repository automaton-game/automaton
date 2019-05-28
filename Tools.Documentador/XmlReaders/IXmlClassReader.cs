using System.Collections.Generic;
using Tools.Documentador.Models;

namespace Tools.Documentador.XmlDocumentation
{
    public interface IXmlClassReader
    {
        IEnumerable<XmlClass> Read();
    }
}