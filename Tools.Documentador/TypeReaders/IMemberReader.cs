using System;
using System.Collections.Generic;
using System.Reflection;
using Tools.Documentador.Dtos;

namespace Tools.Documentador.Readers
{
    public interface IMemberReader
    {
        IMethodInfoDto GetMemeberInfo(MethodInfo methodInfo);
        IItemMemberInfo GetParamInfo(ParameterInfo p);
        IEnumerable<IMethodInfoDto> ReadMembers(Type classType);
    }
}