using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Documentador.Dtos;
using Tools.Documentador.Readers;

namespace Tools.Documentador
{
    public class AssemblyReader : IAssemblyReader
    {
        private readonly IClassReader classReader;

        public AssemblyReader(IClassReader classReader)
        {
            this.classReader = classReader;
        }

        public IEnumerable<IClassInfo> ReadAssembly(Type type)
        {
            var types = type.Assembly.GetExportedTypes();
            var arr = types.Select(classReader.ReadClass);
            return arr;
        }

        public IEnumerable<IClassInfo> ReadAssembly<TClassInAssembly>() where TClassInAssembly : class
        {
            var type = typeof(TClassInAssembly);
            return ReadAssembly(type);
        }
    }
}
