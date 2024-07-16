using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class AuthorResourceRepository : GenericRepository<AuthorResource>
    {
        public AuthorResourceRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
