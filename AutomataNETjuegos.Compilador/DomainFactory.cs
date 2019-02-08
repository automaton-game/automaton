using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;

namespace AutomataNETjuegos.Compilador
{
    public class DomainFactory : IDomainFactory
    {
        private readonly ILogger<DomainFactory> logger;

        public DomainFactory(ILogger<DomainFactory> logger)
        {
            this.logger = logger;
            AssemblyLoadContext.Default.Unloading += Default_Unloading;
        }

        public void Dispose()
        {
            
        }

        public Assembly Load(string filePath)
        {
            
            return AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
        }

        private void Default_Unloading(AssemblyLoadContext obj)
        {
            logger.LogInformation("unloading {0}", obj.ToString());
        }
    }
}
