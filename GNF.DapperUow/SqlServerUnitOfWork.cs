using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Threading;
using GNF.Common.Utility;
using GNF.DapperUow.Repositories;
using GNF.Domain.Entities;

namespace GNF.DapperUow
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlServerUnitOfWork : UnitOfWork
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        public delegate void CommittedDel(bool isSuccess);
        /// <summary>
        /// 
        /// </summary>
        public delegate void RollBackDel();
        
        private readonly string _dbConnectString;
        private IDbConnection _openedConnect;
        private IDbTransaction _openedTransaction;
        /// <summary>
        /// 全局缓存，缓存仓储的构造函数
        /// </summary>
        private static readonly ConcurrentDictionary<string, ConstructorInfo> _constructorDic = new ConcurrentDictionary<string, ConstructorInfo>();
        /// <summary>
        /// 临时缓存，记录当前工作单元上下文中用到的仓储
        /// </summary>
        private readonly Dictionary<string, dynamic> _repositoriesDic = new Dictionary<string, dynamic>();
        private static readonly ConcurrentDictionary<string, FastMethodUtility.FastInvokeHandler> _addMethodDic = new ConcurrentDictionary<string, FastMethodUtility.FastInvokeHandler>();
        private static readonly ConcurrentDictionary<string, FastMethodUtility.FastInvokeHandler> _saveMethodDic = new ConcurrentDictionary<string, FastMethodUtility.FastInvokeHandler>();
        private static readonly ConcurrentDictionary<string, FastMethodUtility.FastInvokeHandler> _removeMethodDic = new ConcurrentDictionary<string, FastMethodUtility.FastInvokeHandler>();
        private static readonly ConcurrentDictionary<string, FastMethodUtility.FastInvokeHandler> _customMethodDic = new ConcurrentDictionary<string, FastMethodUtility.FastInvokeHandler>();

        /// <summary>
        /// 
        /// </summary>
        public event CommittedDel CommittedEvent;
        /// <summary>
        /// 
        /// </summary>
        public event RollBackDel RollBackEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectString"></param>
        public SqlServerUnitOfWork(string connectString)
        {
            _dbConnectString = connectString;
            if (_openedConnect == null)
            {
                Interlocked.CompareExchange(ref _openedConnect, new SqlConnection(_dbConnectString), null);
            }
            CheckCreatedConnectAndEnableTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Commit()
        {
            if (Committed)
            {
                return false;
            }

            bool isSuccess;
            if (_openedConnect != null)
            {
                isSuccess = UseCreatedConnectCommit();
            }
            else
            {
                isSuccess = CreateNewConnectCommit();
            }

            OnCommittedEvent(isSuccess);

            return isSuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Rollback()
        {
            Committed = false;
            _openedConnect?.Dispose();
            _openedTransaction?.Dispose();
            _openedConnect = null;
            _openedTransaction = null;

            try
            {
                OnRollBackEvent();
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override IDbTransaction DbTransaction => _openedTransaction;

        /// <summary>
        /// 
        /// </summary>
        public override IDbConnection DbConnection => _openedConnect;

        /// <summary>
        /// 开启事务
        /// </summary>
        public override void BeginTransaction()
        {
            CheckCreatedConnectAndEnableTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!Committed)
                    Rollback();
                base.Dispose(true);
            }
        }

        /// <summary>
        /// 检查已创建的连接和事务
        /// </summary>
        private void CheckCreatedConnectAndEnableTransaction(bool isEnableTransaction=true)
        {
            if (_openedConnect.State == ConnectionState.Broken
                || _openedConnect.State == ConnectionState.Closed)
            {
                if (string.IsNullOrWhiteSpace(_openedConnect.ConnectionString))
                {
                    _openedConnect.ConnectionString = _dbConnectString;
                }
                _openedConnect.Open();
            }
            if (isEnableTransaction && _openedTransaction == null) _openedTransaction = _openedConnect.BeginTransaction();
        }

        /// <summary>
        /// 使用已创建的连接进行提交
        /// </summary>
        /// <returns></returns>
        private bool UseCreatedConnectCommit()
        {

            using (_openedConnect)
            {
                CheckCreatedConnectAndEnableTransaction();
                using (_openedTransaction)
                {
                    try
                    {
                        foreach (var deletedData in DeletedCollection)
                        {
                            var repository = GetRepostiroy(deletedData.Value, _openedTransaction);
                            var removeMethod = GetRemoveMethod(repository);
                            removeMethod.Invoke(repository, new[] { deletedData.Value });
                        }

                        foreach (var newData in NewCollection)
                        {
                            var repository = GetRepostiroy(newData.Value, _openedTransaction);
                            var addMethod = GetAddMethod(repository);
                            addMethod.Invoke(repository, new[] { newData.Value });
                        }

                        foreach (var modifiedData in ModifiedCollection)
                        {
                            var repository = GetRepostiroy(modifiedData.Value, _openedTransaction);
                            var saveMethod = GetSaveMethod(repository);
                            saveMethod.Invoke(repository, new[] { modifiedData.Value });
                        }

                        foreach (var customOperate in CustomOperateCollection)
                        {
                            var repository = GetRepostiroy(customOperate.Value.RepositoryItemType, _openedTransaction);
                            var method = GetCustomMethod(repository, customOperate.Value.OperateName);
                            method.Invoke(repository, new[] { customOperate.Value.Data });
                        }

                        _openedTransaction.Commit();
                        Committed = true;
                        return true;
                    }
                    catch (Exception)
                    {
                        _openedTransaction.Rollback();
                        Committed = false;
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 创建新连接进行提交
        /// </summary>
        /// <returns></returns>
        private bool CreateNewConnectCommit()
        {
            using (var connection = new SqlConnection(_dbConnectString))
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var deletedData in DeletedCollection)
                        {
                            var repository = GetRepostiroy(deletedData.Value, trans);
                            var removeMethod = GetRemoveMethod(repository);
                            ((FastMethodUtility.FastInvokeHandler)removeMethod).Invoke(repository, new object[] { deletedData.Value });
                        }

                        foreach (var customOperate in CustomOperateCollection)
                        {
                            var repository = GetRepostiroy(customOperate.Value.RepositoryItemType, trans);
                            var method = GetCustomMethod(repository, customOperate.Value.OperateName);
                            ((FastMethodUtility.FastInvokeHandler)method).Invoke(repository, new object[] { customOperate.Value.Data });
                        }

                        foreach (var newData in NewCollection)
                        {
                            var repository = GetRepostiroy(newData.Value, trans);
                            var addMethod = GetAddMethod(repository);
                            ((FastMethodUtility.FastInvokeHandler)addMethod).Invoke(repository, new object[] { newData.Value });
                        }

                        foreach (var modifiedData in ModifiedCollection)
                        {
                            var repository = GetRepostiroy(modifiedData.Value, trans);
                            var saveMethod = GetSaveMethod(repository);
                            ((FastMethodUtility.FastInvokeHandler)saveMethod).Invoke(repository, new object[] { modifiedData.Value });
                        }

                        trans.Commit();
                        Committed = true;
                        return true;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        Committed = false;
                        throw;
                    }
                }
            }
        }

        private dynamic GetRepostiroy(IEntity entity, IDbTransaction sqlTransaction)
        {
            return GetRepostiroy(entity.GetType(), sqlTransaction);
        }

        private dynamic GetRepostiroy(Type entityType, IDbTransaction sqlTransaction)
        {
            var type = entityType;
            dynamic repository;
            if (_repositoriesDic.TryGetValue(type.FullName, out repository))
                return repository;

            var constructor = GetRepostiroyConstructorInfo(entityType);
            var instance = constructor.Invoke(null);
            repository = Convert.ChangeType(instance, constructor.DeclaringType);
            repository.SetDbTransaction(sqlTransaction);
            _repositoriesDic.Add(type.FullName, repository);
            return repository;
        }

        private ConstructorInfo GetRepostiroyConstructorInfo(Type entityType)
        {
            Type generic = typeof(SqlServerRepository<>);
            Type[] typeArgs = { entityType };
            Type realType = generic.MakeGenericType(typeArgs);
            ConstructorInfo constructor;
            if (_constructorDic.TryGetValue(realType.FullName, out constructor))
                return constructor;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(ent => !ent.GlobalAssemblyCache))
            {
                foreach (var type in assembly.GetTypes().Where(ent => ent.BaseType == realType))
                {
                    constructor = type.GetConstructor(new Type[0]);
                    _constructorDic.AddOrUpdate(realType.FullName, constructor, (key, existed) => constructor);
                    return constructor;
                }
            }
            throw new InstanceNotFoundException($"未找到实现{entityType.FullName}的仓储！");
        }

        private FastMethodUtility.FastInvokeHandler GetAddMethod(dynamic repository)
        {
            Type repositoryType = repository.GetType();
            FastMethodUtility.FastInvokeHandler method;
            if (_addMethodDic.TryGetValue(repositoryType.FullName, out method))
                return method;

            MethodInfo addMethod = repositoryType.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
            method = FastMethodUtility.GetMethodInvoker(addMethod);
            _addMethodDic.AddOrUpdate(repositoryType.FullName, method, (key, existed) => method);
            return method;
        }

        private FastMethodUtility.FastInvokeHandler GetSaveMethod(dynamic repository)
        {
            Type repositoryType = repository.GetType();
            FastMethodUtility.FastInvokeHandler method;
            if (_saveMethodDic.TryGetValue(repositoryType.FullName, out method))
                return method;

            MethodInfo saveMethod = repositoryType.GetMethod("Save", BindingFlags.Public | BindingFlags.Instance);
            method = FastMethodUtility.GetMethodInvoker(saveMethod);
            _saveMethodDic.AddOrUpdate(repositoryType.FullName, method, (key, existed) => method);
            return method;
        }

        private FastMethodUtility.FastInvokeHandler GetRemoveMethod(dynamic repository)
        {
            Type repositoryType = repository.GetType();
            FastMethodUtility.FastInvokeHandler method;
            if (_removeMethodDic.TryGetValue(repositoryType.FullName, out method))
                return method;

            MethodInfo removeMethod = repositoryType.GetMethod("Remove", BindingFlags.Public | BindingFlags.Instance);
            method = FastMethodUtility.GetMethodInvoker(removeMethod);
            _removeMethodDic.AddOrUpdate(repositoryType.FullName, method, (key, existed) => method);
            return method;
        }

        private FastMethodUtility.FastInvokeHandler GetCustomMethod(dynamic repository, string methodName)
        {
            Type repositoryType = repository.GetType();
            FastMethodUtility.FastInvokeHandler method;
            if (_customMethodDic.TryGetValue(repositoryType.FullName + methodName, out method))
                return method;

            MethodInfo customMethod = repositoryType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            method = FastMethodUtility.GetMethodInvoker(customMethod);
            _customMethodDic.AddOrUpdate(repositoryType.FullName + methodName, method, (key, existed) => method);
            return method;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="issuccess"></param>
        protected virtual void OnCommittedEvent(bool issuccess)
        {
            CommittedEvent?.Invoke(issuccess);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnRollBackEvent()
        {
            RollBackEvent?.Invoke();
        }
    }
}
