using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tools.Documentador.Dtos;

namespace Tools.Documentador.Readers
{
    public class MemberReader : IMemberReader
    {
        public IEnumerable<IMethodInfoDto> ReadMembers(Type classType)
        {
            var methods = classType
                .GetMethods(
                     BindingFlags.Public |
                     BindingFlags.NonPublic |
                     BindingFlags.Instance |
                     BindingFlags.Static |
                     BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName);
            return methods.Select(GetMemeberInfo);
        }

        public IMethodInfoDto GetMemeberInfo(MethodInfo methodInfo)
        {
            var itemMemberInfo = new MethodInfoDto()
            {
                Name = methodInfo.Name,
                Type = methodInfo.ReturnType.Name,
                Params = methodInfo.GetParameters().Select(GetParamInfo).ToArray()
            };

            return itemMemberInfo;
        }

        public IItemMemberInfo GetParamInfo(ParameterInfo p)
        {
            return new ParamInfo { Name = p.Name, Type = p.ParameterType.Name };
        }
    }
}
