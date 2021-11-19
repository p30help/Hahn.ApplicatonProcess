using System;

namespace Hahn.ApplicationProcess.July2021.Data.Common
{
    public class CommandResult
    {
        public RequestResultTypes ResultType { get; set; }

        public Exception Exception { get; set; }

        public static CommandResult Ok()
        {
            return new CommandResult()
            {
                ResultType = RequestResultTypes.Ok
            };
        }

        public static CommandResult Error(Exception exp)
        {
            return new CommandResult()
            {
                Exception = exp,
                ResultType = RequestResultTypes.Error
            };
        }
    }

    public class CommandResult<TResponse> : CommandResult
    {
        public TResponse Data { get; set; }

        public static CommandResult<TResponse> Ok(TResponse value)
        {
            return  new CommandResult<TResponse>()
            {
                Data = value,
                ResultType = RequestResultTypes.Ok
            };
        }

        public static CommandResult<TResponse> Error(Exception exp)
        {
            return new CommandResult<TResponse>()
            {
                Exception = exp,
                ResultType = RequestResultTypes.Error
            };
        }
    }

    public enum RequestResultTypes
    {
        Ok = 0,
        Error = 1,
        UnhandledError = 2
    }
}
