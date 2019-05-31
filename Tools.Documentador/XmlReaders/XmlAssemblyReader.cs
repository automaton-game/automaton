using System;
using System.Linq;
using Tools.Documentador.Models;
using Tools.Documentador.XmlDocumentation;

namespace Tools.Documentador.XmlReaders
{
    public class XmlAssemblyReader : IXmlAssemblyReader
    {
        private readonly IXmlReaderFactory xmlReaderFactory;

        public XmlAssemblyReader(IXmlReaderFactory xmlReaderFactory)
        {
            this.xmlReaderFactory = xmlReaderFactory;
        }

        public IEnumerableDisposable<XmlClass> Read(Type tipo)
        {
            var enumerableDisposable = new EnumerableDisposable<XmlClass>();
            var existe = xmlReaderFactory.ExisteXml(tipo);

            if (!existe)
            {
                enumerableDisposable.IEnumerable = Enumerable.Empty<XmlClass>();
            }
            else
            {
                var xmlReader = xmlReaderFactory.CreateXmlReader(tipo);
                var xmlClassReader = new XmlClassReader(
                    new XmlMembersReader(xmlReader));
                enumerableDisposable.IEnumerable = xmlClassReader.Read();
            }

            return enumerableDisposable;
        }
    }
}
