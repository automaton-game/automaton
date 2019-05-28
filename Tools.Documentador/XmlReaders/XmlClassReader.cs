using System;
using System.Collections.Generic;
using Tools.Documentador.Models;

namespace Tools.Documentador.XmlDocumentation
{
    public class XmlClassReader : IXmlClassReader
    {
        private readonly XmlMembersReader xmlMembersReader;

        public XmlClassReader(XmlMembersReader xmlMembersReader)
        {
            this.xmlMembersReader = xmlMembersReader;
        }

        public IEnumerable<XmlClass> Read()
        {
            var members = xmlMembersReader.Read();
            XmlClass xmlClass = null;
            IList<XmlMethod> methods = null;

            foreach (var member in members)
            {
                var name = member.Name;
                var nameSplit = name.Split(':');
                var tipo = nameSplit[0];
                var definicion = nameSplit[1];

                switch (tipo)
                {
                    case "T":
                        if(xmlClass != null)
                        {
                            yield return xmlClass;
                        }

                        methods = new List<XmlMethod>();
                        xmlClass = new XmlClass
                        {
                            Methods = methods,
                            Name = definicion,
                            Summary = member.Summary
                        };
                        break;
                    case "M":
                        var xmlMethod = new XmlMethod
                        {
                            Name = definicion,
                            Summary = member.Summary
                        };

                        methods.Add(xmlMethod);
                        break;
                    default:
                        throw new NotSupportedException($"No se reconoce el tipo '{tipo}'");
                }
            }

            if (xmlClass != null)
            {
                yield return xmlClass;
            }
        }

        

    }
}
