using System;
using System.Xml;

namespace Tools.Documentador
{
    public interface IXmlReaderFactory
    {
        XmlReader CreateXmlReader(Type type);
        bool ExisteXml(Type type);
    }
}