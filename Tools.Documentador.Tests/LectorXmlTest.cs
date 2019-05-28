using Automaton.Contratos.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tools.Documentador.Tests
{
    [TestClass]
    public class LectorXmlTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var lectorXml = new LectorXml(new XmlReaderFactory());
            var f = lectorXml.Leer(typeof(RobotHelper), "GetPosition");

            Assert.IsNotNull(f);
        }

        [TestMethod]
        public void GetMembers()
        {
            var lectorXml = new LectorXml(new XmlReaderFactory());
            var f = lectorXml.GetMembers(typeof(RobotHelper), "GetPosition");

            Assert.IsNotNull(f);
        }
    }
}
