using WebReactApi.Core.Infrastructure;
using WebReactApi.Core.Uow;
using Microsoft.EntityFrameworkCore;
using WebReactApi.Core.Repositories;

namespace WebReactApi.Common
{
    public class CrudService<T> : ICrudService<T>
       where T : class, IObjectState

    {
        internal readonly IUnitOfWork UnitOfWork;
        internal readonly IRepository<T> Repository;
        public CrudService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.Repository<Repository<T>>();
        }

        public IList<T> GetAll()
        {
            return Repository.Queryable().ToList();
        }

        public IQueryable<T> Queryable()
        {
            return Repository.Queryable();
        }

        public T Get(params object[] keyValues)
        {
            return Repository.Get(keyValues);
        }

        public void Add(T entity)
        {
            Repository.Insert(entity);
            UnitOfWork.SaveChanges();
        }

        public void Update(T entity)
        {
            Repository.Update(entity);
            UnitOfWork.SaveChanges();
        }

        public void Delete(T entity)
        {
            Repository.Delete(entity);
            UnitOfWork.SaveChanges();
        }

        public void DeleteById(Guid Id)
        {
            var entity = Get(Id);
            if (entity != null)
                Delete(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Repository.Queryable().ToListAsync();
        }

        public async Task<T> GetAsync(params object[] keyValues)
        {
            return await Repository.GetAsync(keyValues);
        }

        public async Task AddAsync(T entity)
        {
            Repository.Insert(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Repository.Update(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Repository.Delete(entity);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid Id)
        {
            var entity = await GetAsync(Id);
            if (entity != null)
                await DeleteAsync(entity);
        }
    }
}
