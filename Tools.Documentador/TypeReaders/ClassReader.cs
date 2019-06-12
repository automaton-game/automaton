using System;
using System.Linq;
using Tools.Documentador.Dtos;

namespace Tools.Documentador.Readers
{
    public class ClassReader : IClassReader
    {
        private readonly IMemberReader memberReader;

        public ClassReader(IMemberReader memberReader)
        {
            this.memberReader = memberReader;
        }

        public IClassInfo ReadClass<TClass>() where TClass : class
        {
            var type = typeof(TClass);
            return ReadClass(type);
        }

        public IClassInfo ReadClass(Type type)
        {
            var classInfo = new ClassInfo
            {
                Name = type.Name,
                Type = type.FullName,
                Methods = memberReader.ReadMembers(type).ToArray(),
                Properties = memberReader.ReadProperties(type).ToArray()
            };

            return classInfo;
        }
    }
}
