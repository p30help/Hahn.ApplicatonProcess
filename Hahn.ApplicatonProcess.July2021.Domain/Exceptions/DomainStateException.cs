using System;

namespace Hahn.ApplicationProcess.July2021.Domain.Exceptions
{
    public abstract class DomainStateException : Exception
    {
        public string[] Parameters { get; set; }

        public DomainStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}