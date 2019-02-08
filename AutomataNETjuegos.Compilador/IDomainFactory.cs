using System;
using System.Reflection;

namespace AutomataNETjuegos.Compilador
{
    public interface IDomainFactory : IDisposable
    {
        Assembly Load(string filePath);
    }
}