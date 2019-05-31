using Tools.Documentador.Readers;
using Tools.Documentador.XmlReaders;

namespace Tools.Documentador
{
    public class ReaderFactory
    {
        public static IAssemblyReader Create()
        {
            var service = new SummaryAssemblyReader(
                new AssemblyReader(
                    new ClassReader(
                        new MemberReader())),
                new XmlAssemblyReader(
                    new XmlReaderFactory()
                    ));

            return service;
        }
    }
}
