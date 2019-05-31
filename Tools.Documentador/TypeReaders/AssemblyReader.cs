using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Documentador.Dtos;
using Tools.Documentador.Readers;
using Tools.Documentador.XmlReaders;

namespace Tools.Documentador
{
    public class AssemblyReader : IAssemblyReader
    {
        private readonly IClassReader classReader;

        public AssemblyReader(IClassReader classReader)
        {
            this.classReader = classReader;
        }

        public IEnumerableDisposable<IClassInfo> ReadAssembly(Type type)
        {
            var types = type.Assembly.GetExportedTypes();
            var arr = types.Select(classReader.ReadClass);
            return new EnumerableDisposable<IClassInfo> { IEnumerable = arr };
        }
    }
}
