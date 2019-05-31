using System;
using System.IO;
using System.Xml;

namespace Tools.Documentador
{
    public class XmlReaderFactory : IXmlReaderFactory
    {
        public bool ExisteXml(Type type)
        {
            var uri = GetPath(type);
            return File.Exists(uri.LocalPath);
        }

        public XmlReader CreateXmlReader(Type type)
        {
            var uri = GetPath(type);
            return XmlReader.Create(uri.AbsoluteUri);
        }
        
        public Uri GetPath(Type type)
        {
            var path = Path.ChangeExtension(type.Assembly.CodeBase, "xml");
            return new Uri(path);
        }
    }
}
