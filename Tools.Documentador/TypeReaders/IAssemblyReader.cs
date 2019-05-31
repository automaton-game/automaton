using System;
using Tools.Documentador.Dtos;
using Tools.Documentador.XmlReaders;

namespace Tools.Documentador
{
    public interface IAssemblyReader
    {
        IEnumerableDisposable<IClassInfo> ReadAssembly(Type type);
    }
}