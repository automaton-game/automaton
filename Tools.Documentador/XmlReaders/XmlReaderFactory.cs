using System;
using System.IO;
using System.Xml;

namespace Tools.Documentador
{
    public class XmlReaderFactory
    {
        public XmlReader CreateXmlReader(Type type)
        {
            var path = Path.ChangeExtension(type.Assembly.CodeBase, "xml");
            return XmlReader.Create(path);
        } 
    }
}
