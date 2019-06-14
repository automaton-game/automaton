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

        public IEnumerable<IItemMemberInfo> ReadProperties(Type classType)
        {
            var methods = classType
                .GetProperties(
                     BindingFlags.Public |
                     BindingFlags.NonPublic |
                     BindingFlags.Instance |
                     BindingFlags.Static |
                     BindingFlags.DeclaredOnly)
                .Where(m => !m.IsSpecialName);
            return methods.Select(GetPropertyInfo);
        }

        public IMethodInfoDto GetMemeberInfo(MethodInfo methodInfo)
        {
            var itemMemberInfo = new MethodInfoDto
            {
                Name = methodInfo.Name,
                Type = GetTypeName(methodInfo.ReturnType),
                Namespace = methodInfo.ReturnType.Namespace,
                Params = methodInfo.GetParameters().Select(GetParamInfo).ToArray()
            };

            return itemMemberInfo;
        }

        public IItemMemberInfo GetPropertyInfo(PropertyInfo propertyInfo)
        {
            var itemMemberInfo = new ItemMemberInfo
            {
                Name = propertyInfo.Name,
                Type = GetTypeName(propertyInfo.PropertyType),
                Namespace = propertyInfo.PropertyType.Namespace
            };

            return itemMemberInfo;
        }

        public IItemMemberInfo GetParamInfo(ParameterInfo p)
        {
            return new ParamInfo
            {
                Name = p.Name,
                Type = GetTypeName(p.ParameterType),
                Namespace = p.ParameterType.Namespace
            };
        }

        private string GetTypeName(Type tipo)
        {
            var rta = tipo.Name;
            var args = tipo.GetGenericArguments().Select(a => a.Name).ToArray();
            if(args.Length > 0)
            {
                rta += string.Concat("<", string.Join(",", args), ">");
            }

            return rta;
        }
        
    }
}
