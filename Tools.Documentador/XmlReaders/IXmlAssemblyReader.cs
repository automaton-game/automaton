using System;
using Tools.Documentador.Models;

namespace Tools.Documentador.XmlReaders
{
    public interface IXmlAssemblyReader
    {
        IEnumerableDisposable<XmlClass> Read(Type type);
    }
}