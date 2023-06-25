namespace WebReactApi.Core.Repositories
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : class
    {
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void AttachDelete(T entity);
    }
}