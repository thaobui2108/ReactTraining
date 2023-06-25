using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebReactApi.Core.Utils;
using WebReactApi.Core.Context;
using WebReactApi.Helper;

namespace WebReactApi.Core.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Guid _instanceId;
        private bool _disposed;
        private Dictionary<Type, dynamic> _repositories;
        private Dictionary<Type, dynamic> _dataServices;
        //private DbContextTransaction _transaction;
        private readonly object _lock = new object();

        protected Dictionary<Type, dynamic> Repositories
        {
            get { return _repositories ?? (_repositories = new Dictionary<Type, dynamic>()); }
        }
        protected Dictionary<Type, dynamic> DataServices
        {
            get { return _dataServices ?? (_dataServices = new Dictionary<Type, dynamic>()); }
        }

        public IDataContext DataContext { get; private set; }

        public UnitOfWork(IDataContext dataContext)
        {
            Guard.ArgumentNotNull("dataContext", dataContext);
            DataContext = dataContext;
            _instanceId = Guid.NewGuid();
        }

        public T Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!Repositories.ContainsKey(type))
            {
                lock (_lock)
                {
                    if (!Repositories.ContainsKey(type))
                    {
                        Repositories.Add(type, DbUtil.Repository<T>(this));
                    }
                }
            }
            return Repositories[type];
        }

        public T DataService<T>() where T : class
        {
            var type = typeof(T);
            if (!DataServices.ContainsKey(type))
            {
                lock (_lock)
                {
                    if (!DataServices.ContainsKey(type))
                    {
                        DataServices.Add(type, DbUtil.DataService<T>(this));
                    }
                }
            }
            return DataServices[type];
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Saves the changes. </summary>
        /// <returns>   An int. </returns>
        /// <see cref="https://msdn.microsoft.com/en-us/data/jj592904"/>
        /// Resolving optimistic concurrency exceptions as client wins
        ///-------------------------------------------------------------------------------------------------

        public int SaveChanges()
        {
            while (true)
            {
                try
                {
                    return DataContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DataContext.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await DataContext.SaveChangesAsync(cancellationToken);
        }

        #region Unit of Work Transactions

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            //var dbContext = DataContext as DbContext;
            //if (dbContext != null)
            //{
            //    EvfmConfiguration.SuspendExecutionStrategy = true;
            //    _transaction = dbContext.Database.BeginTransaction(isolationLevel);
            //}
        }

        public bool Commit()
        {
            //if (_transaction != null)
            //{
            //    _transaction.Commit();
            //    EvfmConfiguration.SuspendExecutionStrategy = false;
            //}
            return true;
        }

        public void Rollback()
        {
            //if (_transaction != null)
            //{
            //    _transaction.Rollback();
            //    if (EvfmConfiguration.SuspendExecutionStrategy)
            //        EvfmConfiguration.SuspendExecutionStrategy = false;
            //}
        }

        #endregion
        public override string ToString()
        {
            // Used for debug
            return _instanceId.ToString();
        }

        #region Destructors

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DataContext.Dispose();
                    DataContext = null;
                }
                _disposed = true;
            }
        }
        #endregion
    }
}
