using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class AuthorResourceRepository : GenericRepository<AuthorResource>, IAuthorResourceRepository
    {
        public AuthorResourceRepository(LibraryDbContext context) : base(context)
        {
        }

       public async Task<AuthorResource?> FindByAuthorAndResourceAsync(Author author, Resource resource)
        {
            return await context.AuthorResources.FirstOrDefaultAsync(
                    ar => ar.AuthorId == author.AuthorId && ar.ResourceId == resource.ResourceId
            );
        }
    }
}
