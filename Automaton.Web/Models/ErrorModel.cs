namespace Automaton.Web.Models
{
    public class ErrorModel
    {
        public string Name { get; set; }

        public int HResult { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }
    }
}
