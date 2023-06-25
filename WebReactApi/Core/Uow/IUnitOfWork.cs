using WebReactApi.Core.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebReactApi.Core.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IDataContext DataContext { get; }
        T Repository<T>() where T : class;
        T DataService<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}
