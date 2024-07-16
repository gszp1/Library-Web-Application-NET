using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class ReservationRepository : GenericRepository<Reservation>
    {
        public ReservationRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
