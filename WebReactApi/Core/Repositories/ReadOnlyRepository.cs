using Microsoft.EntityFrameworkCore;
using WebReactApi.Core.Context;
using WebReactApi.Core.Uow;
using WebReactApi.Helper;

namespace WebReactApi.Core.Repositories
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
    {
        public readonly IUnitOfWork TheUnitOfWork;
        protected IDataContext DbContext { get; private set; }
        protected DbSet<T> DbSet { get; private set; }

        protected ReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            Guard.ArgumentNotNull("unitOfWork", unitOfWork);
            Guard.ArgumentNotNull("unitOfWork.DataContext", unitOfWork.DataContext);

            TheUnitOfWork = unitOfWork;
            DbContext = TheUnitOfWork.DataContext;
            DbSet = DbContext.Set<T>();
        }

        protected DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return DbContext.Set<TEntity>();
        }

        public T Get(params object[] keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public async Task<T> GetAsync(params object[] keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        public IQueryable<T> Queryable()
        {
            return DbSet;
        }

        public Task<T1> FirstOrDefaultAsync<T1>(IQueryable<T1> query)
        {
            return query.FirstOrDefaultAsync();
        }

        public Task<T1> SingleOrDefaultAsync<T1>(IQueryable<T1> query)
        {
            return query.SingleOrDefaultAsync();
        }

        public Task<List<T1>> ToListAsync<T1>(IQueryable<T1> query)
        {
            return query.ToListAsync();
        }
    }
}