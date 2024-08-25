using Library_Web_Application_NET.Server.src.data.context;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        LibraryDbContext Context { get; }

        IAuthorRepository Authors { get; }
        
        IAuthorResourceRepository AuthorResources { get; }

        IPublisherRepository Publishers { get; }

        IReservationRepository Reservations { get; }

        IResourceInstanceRepository ResourceInstances { get; }

        IResourceRepository Resources { get; }

        IUserRepository Users { get; }

        Task<int> CompleteAsync();
    }
}
