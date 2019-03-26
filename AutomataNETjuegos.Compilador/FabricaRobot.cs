using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using AutomataNETjuegos.Compilador.Excepciones;
using AutomataNETjuegos.Contratos.Robots;
using AutomataNETjuegos.Logica;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AutomataNETjuegos.Compilador
{
    public class FabricaRobot : IFabricaRobot
    {
        private readonly ITempFileManager tempFileManager;
        private readonly IDomainFactory domainFactory;
        private readonly IMetadataFactory metadataFactory;

        public FabricaRobot(
            ITempFileManager tempFileManager,
            IDomainFactory domainFactory,
            IMetadataFactory metadataFactory)
        {
            this.tempFileManager = tempFileManager;
            this.domainFactory = domainFactory;
            this.metadataFactory = metadataFactory;
        }

        public IRobot ObtenerRobot(Type tipo)
        {
            return (IRobot)Activator.CreateInstance(tipo);
        }

        public IRobot ObtenerRobot(string t)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(t);
            string assemblyName = Path.GetRandomFileName();
            var references = metadataFactory.GetReferences();

            var compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var tempFile = tempFileManager.Create();
            var result = compilation.Emit(tempFile);

            if (!result.Success)
            {
                IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                var errores = failures.Select(f => f.ToString()).ToArray();
                throw new ExcepcionCompilacion { ErroresCompilacion = errores };
            }
            else
            {
                var assembly = domainFactory.Load(tempFile);
                var type = assembly.ExportedTypes.FirstOrDefault(tipo =>
                    tipo.IsClass && tipo.IsPublic && tipo.IsVisible && typeof(IRobot).IsAssignableFrom(tipo));
                
                return ObtenerRobot(type);
            }
        }
    }
}
