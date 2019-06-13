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
            var service = ReaderFactory.Create();

            using (var ie = service.ReadNamespaces(typeAssembly))
            {
                var rta = ie.ToList();
                Assert.IsNotNull(rta);
            }
        }
    }
}
