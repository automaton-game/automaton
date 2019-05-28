using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Documentador.Dtos;
using Tools.Documentador.XmlDocumentation;

namespace Tools.Documentador
{
    public class SummaryAssemblyReader
    {
        private readonly IAssemblyReader assemblyReader;
        private readonly IXmlClassReader xmlClassReader;

        public SummaryAssemblyReader(
            IAssemblyReader assemblyReader,
            IXmlClassReader xmlClassReader)
        {
            this.assemblyReader = assemblyReader;
            this.xmlClassReader = xmlClassReader;
        }

        public IEnumerable<IClassInfo> ReadAssembly(Type type)
        {
            var classInfos = assemblyReader.ReadAssembly(type);
            var xmlClasses = xmlClassReader.Read();

            var qry =
                from classInfo in classInfos
                join xmlClass in xmlClasses.DefaultIfEmpty() on classInfo.Type equals xmlClass.Name into xmlClassesJoin
                from xmlClassJoin in xmlClassesJoin.DefaultIfEmpty()
                select new ClassSummaryMember<IClassInfo>()
                {
                    Summary = xmlClassJoin?.Summary,
                    ItemMemberInfo = classInfo
                };

            return qry;
        }
    }
}
