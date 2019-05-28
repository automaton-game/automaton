using System;
using System.Collections.Generic;
using Tools.Documentador.Dtos;

namespace Tools.Documentador
{
    public interface IAssemblyReader
    {
        IEnumerable<IClassInfo> ReadAssembly(Type type);
        IEnumerable<IClassInfo> ReadAssembly<TClassInAssembly>() where TClassInAssembly : class;
    }
}