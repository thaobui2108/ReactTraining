using WebReactApi.Core.Infrastructure;
using WebReactApi.Core.Uow;

namespace WebReactApi.Core.Repositories
{
    /// <summary>
    /// Object graph repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class, IObjectState
    {
        public Repository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Insert(T entity)
        {
            entity.ObjectState = ObjectState.Added;
            DbSet.Add(entity);
        }

        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public void Update(T entity)
        {
            entity.ObjectState = ObjectState.Modified;
            DbSet.Attach(entity);
            //MarkAsModified(entity);
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public void Delete(T entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            DbSet.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public void AttachDelete(T entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public void AttachDelete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                AttachDelete(entity);
            }
        }

        //private void MarkAsModified(T entity)
        //{
        //    var objectContextAdapter = DbContext as IObjectContextAdapter;
        //    objectContextAdapter?.ObjectContext.ObjectStateManager
        //        .ChangeObjectState(entity, EntityState.Modified);
        //}
    }
}