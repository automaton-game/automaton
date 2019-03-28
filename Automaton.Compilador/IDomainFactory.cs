using System;
using System.Reflection;

namespace Automaton.Compilador
{
    public interface IDomainFactory : IDisposable
    {
        Assembly Load(string filePath);
    }
}