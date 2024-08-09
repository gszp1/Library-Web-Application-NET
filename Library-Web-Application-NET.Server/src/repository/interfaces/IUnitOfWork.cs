namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        
        IAuthorResourceRepository AuthorResources { get; }

        IPublisherRepository Publishers { get; }

        IReservationRepository Reservations { get; }

        IResourceInstanceRepository ResourceInstances { get; }

        IResourceRepository Resources { get; }

        IUserRepository Users { get; }

        int Complete();
    }
}
