using System;

namespace GNF.Domain.UnitOfWork
{
    public class UnitOfWorkCompleteEventArgs : EventArgs
    {
        public UnitOfWorkCompleteEventArgs(TimeSpan executeTimeSpan)
        {
            ExecuteTimeSpan = executeTimeSpan;
        }

        public TimeSpan ExecuteTimeSpan { get; internal set; }
    }
}
