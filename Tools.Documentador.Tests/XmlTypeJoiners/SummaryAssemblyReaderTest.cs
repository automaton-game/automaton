using Automaton.Contratos.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.Documentador.Readers;
using Tools.Documentador.XmlDocumentation;

namespace Tools.Documentador.Tests.XmlTypeJoiners
{
    [TestClass]
    public class SummaryAssemblyReaderTest
    {
        [TestMethod]
        public void SummaryAssemblyReaderRead()
        {
            var typeAssembly = typeof(RobotHelper);
            var service = new SummaryAssemblyReader(
                new AssemblyReader(
                    new ClassReader(
                        new MemberReader())),
                new XmlClassReader(
                    new XmlMembersReader(
                        new XmlReaderFactory().CreateXmlReader(typeAssembly))));

            var rta = service.ReadAssembly(typeAssembly).ToList();

            Assert.IsNotNull(rta);
        }
    }
}
