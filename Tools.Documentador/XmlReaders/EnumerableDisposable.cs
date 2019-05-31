using System;
using System.Collections;
using System.Collections.Generic;

namespace Tools.Documentador.XmlReaders
{
    public class EnumerableDisposable<T> : IEnumerableDisposable<T>
    {
        public IEnumerable<T> IEnumerable { get; set; }

        public IDisposable Disposable { get; set; }

        public void Dispose()
        {
            if(Disposable != null)
            {
                Disposable.Dispose();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return IEnumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return IEnumerable.GetEnumerator();
        }
    }
}
