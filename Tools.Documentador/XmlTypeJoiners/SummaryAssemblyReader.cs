using System;
using System.Linq;
using Tools.Documentador.Dtos;
using Tools.Documentador.Models;
using Tools.Documentador.XmlReaders;
using Tools.Documentador.XmlTypeJoiners.Dtos;

namespace Tools.Documentador
{
    public class SummaryAssemblyReader : IAssemblyReader
    {
        private readonly IAssemblyReader assemblyReader;
        private readonly IXmlAssemblyReader xmlAssemblyReader;

        public SummaryAssemblyReader(
            IAssemblyReader assemblyReader,
            IXmlAssemblyReader xmlAssemblyReader)
        {
            this.assemblyReader = assemblyReader;
            this.xmlAssemblyReader = xmlAssemblyReader;
        }

        public IEnumerableDisposable<IClassInfo> ReadAssembly(Type type)
        {
            var classInfos = assemblyReader.ReadAssembly(type);
            var xmlClasses = xmlAssemblyReader.Read(type);
            var qry =
                from classInfo in classInfos
                join xmlClass in xmlClasses on classInfo.Type equals xmlClass.Name into xmlClassesJoin
                from xmlClassJoin in xmlClassesJoin.DefaultIfEmpty()
                select GetClassSummaryMember(classInfo, xmlClassJoin);

            return new EnumerableDisposable<IClassInfo>
            {
                Disposable = xmlClasses,
                IEnumerable = qry
            };
        }

        private IClassInfo GetClassSummaryMember(IClassInfo classInfo, XmlClass xmlClass)
        {
            if(xmlClass == null)
            {
                return classInfo;
            }

            var qry =
                from typeMethod in classInfo.Items.DefaultIfEmpty()
                join xmlMethod in xmlClass.Methods on GetXmlMethodName(classInfo, typeMethod) equals xmlMethod.Name into xmlMethodsLeft
                from xmlMethod in xmlMethodsLeft.DefaultIfEmpty()
                select CreateSummary(typeMethod, xmlMethod);
            var items = qry.ToArray();

            var newClassInfo = new ClassInfo
            {
                Items = items,
                Name = classInfo.Name,
                Type = classInfo.Type
            };


            return new ClassSummaryMember<IClassInfo>()
            {
                Summary = xmlClass?.Summary,
                ItemMemberInfo = newClassInfo
            };

        }

        private IMethodInfoDto CreateSummary<TMember>(TMember member, XmlMember xmlMember) where TMember : IMethodInfoDto
        {
            if (xmlMember == null)
            {
                return member;
            }

            return new MethodSummaryMember<TMember>
            {
                ItemMemberInfo = member,
                Summary = xmlMember?.Summary
            };
        }

        private string GetXmlMethodName(IClassInfo classinfo, IMethodInfoDto methodInfo)
        {
            var parametros = methodInfo.Params.Select(p => p.Type);
            var pJoin = string.Join(",", parametros);
            string methodName = classinfo.Type + "." + methodInfo.Name + "(" + pJoin + ")";
            return methodName;
        }
    }
}
