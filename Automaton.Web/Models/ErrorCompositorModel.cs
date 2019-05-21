using System.Collections.Generic;

namespace Automaton.Web.Models
{
    public class ErrorCompositorModel : ErrorModel
    {
        public IList<ErrorModel> Errors { get; set; }
    }
}
