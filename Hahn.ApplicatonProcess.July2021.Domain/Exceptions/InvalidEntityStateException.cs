namespace Hahn.ApplicationProcess.July2021.Domain.Exceptions
{
    public class InvalidEntityStateException : DomainStateException
    {
        public InvalidEntityStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}