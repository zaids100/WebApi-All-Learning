namespace API.Application
{
    public interface IUserPost<TEntity, TKey> where TEntity : class
                                         where TKey : notnull, IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(TKey id);

        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetByName(string name);
    }
}
