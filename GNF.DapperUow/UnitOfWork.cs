using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using GNF.Domain.Entities;

namespace GNF.DapperUow
{
    /// <summary>
    /// Represents the base class for repository contexts.
    /// </summary>
    public abstract class UnitOfWork : DisposableObject, IUnitOfWork
    {
        #region Private Fields
        private readonly ThreadLocal<Dictionary<string, IEntity>> _localNewCollection = new ThreadLocal<Dictionary<string, IEntity>>(() => new Dictionary<string, IEntity>());

        private readonly ThreadLocal<Dictionary<string, IEntity>> _localModifiedCollection = new ThreadLocal<Dictionary<string, IEntity>>(() => new Dictionary<string, IEntity>());

        private readonly ThreadLocal<Dictionary<string, IEntity>> _localDeletedCollection = new ThreadLocal<Dictionary<string, IEntity>>(() => new Dictionary<string, IEntity>());

        private readonly ThreadLocal<Dictionary<string, CustomOperate<IEntity>>> _localCustomOperateCollection = new ThreadLocal<Dictionary<string, CustomOperate<IEntity>>>(() => new Dictionary<string, CustomOperate<IEntity>>());

        private readonly ThreadLocal<bool> _localCommitted = new ThreadLocal<bool>(() => false);
        #endregion

        #region Protected Methods
        /// <summary>
        /// Clears all the registration in the repository context.
        /// </summary>
        /// <remarks>Note that this can only be called after the repository context has successfully committed.</remarks>
        protected void ClearRegistrations()
        {
            _localNewCollection.Value.Clear();
            _localModifiedCollection.Value.Clear();
            _localDeletedCollection.Value.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _localCommitted.Dispose();
                _localDeletedCollection.Dispose();
                _localModifiedCollection.Dispose();
                _localNewCollection.Dispose();
            }
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be added to the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<string, IEntity>> NewCollection => _localNewCollection.Value;

        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be modified in the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<string, IEntity>> ModifiedCollection => _localModifiedCollection.Value;

        /// <summary>
        /// Gets an enumerator which iterates over the collection that contains all the objects need to be deleted from the repository.
        /// </summary>
        protected IEnumerable<KeyValuePair<string, IEntity>> DeletedCollection => _localDeletedCollection.Value;

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<KeyValuePair<string, CustomOperate<IEntity>>> CustomOperateCollection => _localCustomOperateCollection.Value;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public abstract IDbConnection DbConnection { get; }

        /// <summary>
        /// 尝试锁住对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool LockObject<TEntity>(int timeout, params object[] id) where TEntity : IEntity
        {
            return id.Select(uid => DbConnection.LockRow<TEntity>(uid, DbTransaction, timeout)).All(result => result);
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void BeginTransaction();

        /// <summary>
        /// Registers a new object to the repository context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterAdded<TEntity>(TEntity obj)
            where TEntity : IEntity
        {
            var objId = EntityAttributeUtil.GetId(obj);
            if (string.IsNullOrEmpty(objId))
                throw new ArgumentException("The UniqueIdentifier of the object is empty.", nameof(obj));

            if (_localModifiedCollection.Value.ContainsKey(objId))
                throw new InvalidOperationException(
                    "The object cannot be registered as a new object since it was marked as modified.");

            if (_localNewCollection.Value.ContainsKey(objId))
                throw new InvalidOperationException("The object has already been registered as a new object.");

            _localNewCollection.Value.Add(objId, obj);
            _localCommitted.Value = false;
        }

        /// <summary>
        /// Registers a modified object to the repository context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterModified<TEntity>(TEntity obj)
            where TEntity : IEntity
        {
            var objId = EntityAttributeUtil.GetId(obj);
            if (string.IsNullOrEmpty(objId))
                throw new ArgumentException("The UniqueIdentifier of the object is empty.", nameof(obj));

            if (_localDeletedCollection.Value.ContainsKey(objId))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");

            if (!_localModifiedCollection.Value.ContainsKey(objId) && !_localNewCollection.Value.ContainsKey(objId))
                _localModifiedCollection.Value.Add(objId, obj);

            _localCommitted.Value = false;
        }

        /// <summary>
        /// Registers a deleted object to the repository context.
        /// </summary>
        /// <typeparam name="TEntity">The type of the aggregate root.</typeparam>
        /// <param name="obj">The object to be registered.</param>
        public virtual void RegisterRemoved<TEntity>(TEntity obj)
            where TEntity : IEntity
        {
            var objId = EntityAttributeUtil.GetId(obj);
            if (objId.Equals(string.Empty))
                throw new ArgumentException("The UniqueIdentifier of the object is empty.", nameof(obj));

            if (_localNewCollection.Value.ContainsKey(objId))
            {
                if (_localNewCollection.Value.Remove(objId))
                    return;
            }

            bool removedFromModified = _localModifiedCollection.Value.Remove(objId);
            bool addedToDeleted = false;
            if (!_localDeletedCollection.Value.ContainsKey(objId))
            {
                _localDeletedCollection.Value.Add(objId, obj);
                addedToDeleted = true;
            }

            _localCommitted.Value = !(removedFromModified || addedToDeleted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        /// <param name="repositoryItemType"></param>
        /// <param name="operateName"></param>
        public void RegisterCustomOperate<TEntity>(TEntity obj, Type repositoryItemType, string operateName) where TEntity : IEntity
        {
            var objId = EntityAttributeUtil.GetId(obj);
            if (string.IsNullOrEmpty(objId)) throw new ArgumentException("The UniqueIdentifier of the object is empty.", nameof(obj));
            if (_localDeletedCollection.Value.ContainsKey(objId)) throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!_localCustomOperateCollection.Value.ContainsKey(objId) && !_localNewCollection.Value.ContainsKey(objId)) _localCustomOperateCollection.Value.Add(objId, new CustomOperate<IEntity>(obj, repositoryItemType, operateName));
            _localCommitted.Value = false;
        }

        /// <summary>
        /// Gets a <see cref="System.Boolean"/> value which indicates whether the UnitOfWork
        /// was committed.
        /// </summary>
        public bool Committed
        {
            get => _localCommitted.Value;
            protected set => _localCommitted.Value = value;
        }

        /// <summary>
        /// Commits the UnitOfWork.
        /// </summary>
        public abstract bool Commit();

        /// <summary>
        /// Rolls-back the UnitOfWork.
        /// </summary>
        public abstract void Rollback();

        /// <summary>
        /// 
        /// </summary>
        public abstract IDbTransaction DbTransaction { get; }
    }
}
