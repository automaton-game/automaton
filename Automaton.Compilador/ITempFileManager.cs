using System;

namespace Automaton.Compilador
{
    public interface ITempFileManager : IDisposable
    {
        string Create();
    }
}