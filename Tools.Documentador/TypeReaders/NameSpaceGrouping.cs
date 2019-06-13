using System;
using System.Linq;
using Tools.Documentador.TypeReaders.Dtos;
using Tools.Documentador.XmlReaders;

namespace Tools.Documentador.TypeReaders
{
    public class NameSpaceGrouping : INameSpaceGrouping
    {
        private readonly IAssemblyReader assemblyReader;

        public NameSpaceGrouping(IAssemblyReader assemblyReader)
        {
            this.assemblyReader = assemblyReader;
        }

        public IEnumerableDisposable<NameSpaceInfo> ReadNamespaces(Type type)
        {
            var classes = assemblyReader.ReadAssembly(type);
            var ie = classes
                .GroupBy(c => c.Namespace)
                .OrderBy(c => c.Key)
                .Select(c => new NameSpaceInfo { Classes = c.ToArray(), Name = c.Key });
            return new EnumerableDisposable<NameSpaceInfo>
            {
                Disposable = classes,
                IEnumerable = ie,
            };
        }
    }
}
