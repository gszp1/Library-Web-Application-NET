using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class AuthorRepository : GenericRepository<Author>
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
