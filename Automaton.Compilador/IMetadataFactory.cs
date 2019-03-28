using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Automaton.Compilador
{
    public interface IMetadataFactory
    {
        ICollection<MetadataReference> GetReferences();
    }
}