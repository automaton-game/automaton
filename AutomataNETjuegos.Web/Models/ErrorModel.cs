using System.Collections.Generic;

namespace AutomataNETjuegos.Web.Models
{
    public class ErrorModel
    {
        public IList<string> Errores { get; set; }

        public string Name { get; set; }

        public int HResult { get; set; }

        public string Message { get; set; }

        public string Stack { get; set; }
    }
}
