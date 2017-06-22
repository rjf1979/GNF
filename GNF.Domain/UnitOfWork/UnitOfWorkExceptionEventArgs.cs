using System;

namespace GNF.Domain.UnitOfWork
{
    public class UnitOfWorkExceptionEventArgs: EventArgs
    {
        public UnitOfWorkExceptionEventArgs(TimeSpan executeTimeSpan, Exception exception)
        {
            Exception = exception;
            ExecuteTimeSpan = executeTimeSpan;
        }

        public Exception Exception { get; internal set; }

        public TimeSpan ExecuteTimeSpan { get; internal set; }
    }
}
