using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author> FindByEmailAsync(string email);

        Task<Author> FindByAuthorIdAsync(int authorId);

        Task<List<Author>> FindByEmailsAsync(List<string> emails);
    }
}
