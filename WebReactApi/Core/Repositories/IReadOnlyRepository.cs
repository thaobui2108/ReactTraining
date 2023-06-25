namespace WebReactApi.Core.Repositories
{
    public interface IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        /// Get entity domain by primary keys
        /// </summary>
        /// <param name="keyValues">Key values by database order</param>
        /// <returns>Entity</returns>
        T Get(params object[] keyValues);

        /// <summary>
        /// Get entity domain by primary keys
        /// </summary>
        /// <param name="keyValues">Key values by database order</param>
        /// <returns>Entity</returns>
        Task<T> GetAsync(params object[] keyValues);
        IQueryable<T> Queryable();

    }
}