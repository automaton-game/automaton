using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Automaton.Compilador
{
    public class TempFileManager : ITempFileManager
    {
        private readonly ILogger logger;
        private List<string> tempFiles;

        public TempFileManager(ILogger<TempFileManager> logger)
        {
            tempFiles = new List<string>();
            this.logger = logger;
        }

        public string Create()
        {
            var tempFilePath = Path.GetTempFileName();
            logger.LogInformation("Genero archivo temporal {0}", tempFilePath);
            this.tempFiles.Add(tempFilePath);
            return tempFilePath;
        }

        public void Dispose()
        {
            try
            {
                foreach (var tempFile in tempFiles)
                {

                    try
                    {
                        File.Delete(tempFile);
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning(ex, "Fallo al intentar eliminar archivo temporal {0}", tempFile);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
