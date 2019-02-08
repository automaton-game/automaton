using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace AutomataNETjuegos.Compilador
{
    public interface IMetadataFactory
    {
        ICollection<MetadataReference> GetReferences();
    }
}