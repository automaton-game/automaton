using Automaton.Contratos.Robots;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Reflection;

namespace Automaton.Compilador
{
    public class MetadataFactory : IMetadataFactory
    {
        private ICollection<MetadataReference> references = null;

        public ICollection<MetadataReference> GetReferences()
        {
            if (this.references == null)
            { 
                var references = new MetadataReference[]
                {
                    MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(IRobot).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).GetTypeInfo().Assembly.Location)
                };

                this.references = references;
            }
            return this.references;
        }
    }
}
