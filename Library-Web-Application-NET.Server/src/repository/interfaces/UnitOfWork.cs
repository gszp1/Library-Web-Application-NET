using Library_Web_Application_NET.Server.src.data.context;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext context;

        public LibraryDbContext Context { get { return context; } }
        
        public IAuthorRepository Authors { get; private set; }

        public IAuthorResourceRepository AuthorResources { get; private set; }

        public IPublisherRepository Publishers { get; private set; }

        public IReservationRepository Reservations { get; private set; }

        public IResourceInstanceRepository ResourceInstances { get; private set; }

        public IResourceRepository Resources { get; private set; }

        public IUserRepository Users { get; private set; }

        public UnitOfWork(LibraryDbContext context)
        {
            this.context = context;
            this.Authors = new AuthorRepository(context);
            this.AuthorResources = new AuthorResourceRepository(context);
            this.Publishers = new PublisherRepository(context);
            this.Reservations = new ReservationRepository(context);
            this.ResourceInstances = new ResourceInstanceRepository(context);
            this.Resources = new ResourceRepository(context);
            this.Users = new UserRepository(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
