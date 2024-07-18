namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
