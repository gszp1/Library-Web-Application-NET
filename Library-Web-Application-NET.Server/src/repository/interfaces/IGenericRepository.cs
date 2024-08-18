﻿namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> FindByIdAsync(int id);

        Task<IEnumerable<T>> FindAllAsync();

        Task SaveAsync(T entity);

        Task SaveAllAsync(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
