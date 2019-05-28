using System;
using System.Collections.Generic;
using System.Xml;
using Tools.Documentador.Models;

namespace Tools.Documentador
{
    public class XmlMembersReader
    {
        private readonly XmlReader xmlReader;

        public XmlMembersReader(XmlReader xmlReader)
        {
            this.xmlReader = xmlReader;
        }

        public IEnumerable<XmlMember> Read()
        {
            XmlMember memberDataBase = null;

            while (xmlReader.Read())
            {
                // Only detect start elements.
                if (xmlReader.IsStartElement())
                {
                    
                    switch (xmlReader.Name)
                    {
                        case "member":
                            // Search for the attribute name on this current node.
                            memberDataBase = new XmlMember
                            {
                               Name = xmlReader["name"]
                            };

                            //var nameSplit = name.Split(':');
                            //var tipo = nameSplit[0];
                            //var definicion = nameSplit[1];

                            ////switch (tipo)
                            ////{
                            ////    case "T":
                            ////        memberDataBase = new ClassMember();
                            ////        break;
                            ////    case "M":
                            ////        memberDataBase = new MethodMember();
                            ////        break;
                            ////    default:
                            ////        throw new NotSupportedException($"No se reconoce el tipo '{tipo}'");
                            ////}

                            //memberDataBase.Name = definicion;

                            break;
                        case "summary":
                            // Next read will contain text.
                            if (xmlReader.Read())
                            {
                                memberDataBase.Summary = xmlReader.Value.Trim();
                            }

                            yield return memberDataBase;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
