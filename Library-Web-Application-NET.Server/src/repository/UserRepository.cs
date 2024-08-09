using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context) : base(context) { }
    }
}
