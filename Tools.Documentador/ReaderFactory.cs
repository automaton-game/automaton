using Tools.Documentador.Readers;
using Tools.Documentador.TypeReaders;
using Tools.Documentador.XmlReaders;

namespace Tools.Documentador
{
    public class ReaderFactory
    {
        public static INameSpaceGrouping Create()
        {
            var service = new NameSpaceGrouping(
                new SummaryAssemblyReader(
                    new AssemblyReader(
                        new ClassReader(
                            new MemberReader())),
                    new XmlAssemblyReader(
                        new XmlReaderFactory()
                        )));

            return service;
        }
    }
}
