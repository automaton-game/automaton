using Automaton.Contratos.Robots;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Tools.Documentador.Models;

namespace Tools.Documentador
{
    public class LectorXml
    {
        private readonly XmlReaderFactory xmlReaderFactory;

        public LectorXml(XmlReaderFactory xmlReaderFactory)
        {
            this.xmlReaderFactory = xmlReaderFactory;
        }

        public IEnumerable<XmlMember> GetMembers(Type type, string m)
        {
            MethodInfo method = type.GetMethod(m, new[] { typeof(IRobot) });

            string typeName = "T:" + type.FullName;
            string methodName = "M:" + type.FullName + "." + method.Name + "(" + string.Join(",", method.GetParameters().Select(p => p.ParameterType.FullName).ToArray()) + ")";
            string typeDoc = null;
            string methodDoc = null;


            using (var xmlReader = this.xmlReaderFactory.CreateXmlReader(type))
            {
                var xmlMemberReader = new XmlMembersReader(xmlReader);
                var members = xmlMemberReader.Read().ToList();

                xmlReader.Close();
                return members;
            }
        }

        public string Leer(Type type, string m)
        {
            //Type type = typeof(T);
            MethodInfo method = type.GetMethod(m, new[] { typeof(IRobot) });

            string typeName = "T:" + type.FullName;
            string methodName = "M:" + type.FullName + "." + method.Name + "(" + string.Join(",", method.GetParameters().Select(p => p.ParameterType.FullName).ToArray()) + ")";
            string typeDoc = null;
            string methodDoc = null;

            
            XDocument doc = XDocument.Load(Path.ChangeExtension(type.Assembly.CodeBase, "xml"));
            var test = doc.Root.Element("members").Elements("member").Attributes();
            XElement typeDocElement = doc.Root.Element("members").Elements("member").FirstOrDefault(e => e.Attribute("name").Value == typeName);
            if (typeDocElement != null)
                typeDoc = typeDocElement.Element("summary").Value.Trim('\n', ' ');
            XElement methodDocElement = doc.Root.Element("members").Elements("member").FirstOrDefault(e => e.Attribute("name").Value == methodName);
            if (methodDocElement != null)
                methodDoc = methodDocElement.Element("summary").Value.Trim('\n', ' '); ;

            return methodDoc;

        }
    }
}
