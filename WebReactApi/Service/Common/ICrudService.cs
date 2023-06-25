namespace WebReactApi.Common
{
    public interface ICrudService<T> where T : class
    {
        IList<T> GetAll();
        T Get(params object[] keyValues);
        IQueryable<T> Queryable();

        void Add(T entity);
        void Update(T entity);

        void Delete(T entity);
        void DeleteById(Guid Id);

        Task<IList<T>> GetAllAsync();
        Task<T> GetAsync(params object[] keyValues);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(Guid Id);

    }
}
