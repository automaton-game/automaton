using System;
using Tools.Documentador.TypeReaders.Dtos;
using Tools.Documentador.XmlReaders;

namespace Tools.Documentador.TypeReaders
{
    public interface INameSpaceGrouping
    {
        IEnumerableDisposable<NameSpaceInfo> ReadNamespaces(Type type);
    }
}