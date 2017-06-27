using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// Defines a unit of work.
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork
    {
        /// <summary>
        /// Unique id of this UOW.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Begins the unit of work with given options.
        /// </summary>
        void Begin();

        /// <summary>
        /// Completes this unit of work.
        /// It saves all changes and commit transaction if exists.
        /// </summary>
        void Complete();

        /// <summary>
        /// Completes this unit of work.
        /// It saves all changes and commit transaction if exists.
        /// </summary>
        Task CompleteAsync();
    }
}
