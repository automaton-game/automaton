using System;
using System.Collections.Generic;

namespace Tools.Documentador.XmlReaders
{
    public interface IEnumerableDisposable<T> : IEnumerable<T>, IDisposable
    {
    }
}