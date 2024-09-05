using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.statistics;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<int> CountUserResourceReservationsWithStatusAsync(
            int resourceId,
            string userEmail,
            List<ReservationStatus> statuses
        ) {
            return await context
                .Users
                .Where(u => u.Email.Equals(userEmail))
                .SelectMany(u => u.Reservations)
                .Where(r => r.Instance.Resource.ResourceId == resourceId && statuses.Contains(r.Status))
                .CountAsync();
        }

        public async Task<List<Reservation>> FindAllByReservationStatusWithInstancesAsync(
            List<ReservationStatus> statuses
        )
        {
            return await context
                .Reservations
                .Include(r => r.Instance)
                .Where(r => statuses.Contains(r.Status))
                .ToListAsync();
        }

        public async Task<Reservation?> FindByReservationIdWithInstanceAsync(int reservationId)
        {
            return await context
                .Reservations
                .Include(r => r.Instance)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
        }
        
        public async Task<List<Reservation>> FindAllByUserEmailWithInstancesAsync(string userEmail)
        {
            return await context
                .Reservations
                .Include(r => r.Instance)
                .ThenInclude(i => i.Resource)
                .Include(r => r.User)
                .Where(r => r.User.Email.Equals(userEmail))
                .ToListAsync();
        }

        public async Task<List<Reservation>> FindAllByUserEmailAndReservationStatusWithInstancesAsync(
            string email,
            ReservationStatus status
        )
        {
            return await context
                .Reservations
                .Include(r => r.Instance)
                .Where(r => r.Status == status && r.User.Email.Equals(email))
                .ToListAsync();
        }

        public async Task<List<Reservation>> FindAllWithDataAsync(string sortBy, bool descending)
        {
            var query = context
                .Reservations
                .Include(r => r.User)
                .Include(r => r.Instance)
                .ThenInclude(i => i.Resource)
                .AsQueryable();
            if (!string.IsNullOrEmpty(sortBy))
            {
                var param = Expression.Parameter(typeof(Reservation), "r");
                var property = typeof(Reservation).GetProperty(sortBy);
                if (property != null)
                {
                    var sortExpression = Expression.Lambda(Expression.Property(param, property), param);

                    var methodName = descending ? "OrderByDescending" : "OrderBy";

                    var resultExpression = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new Type[] {typeof(Reservation), property.PropertyType},
                        query.Expression,
                        sortExpression);
                    query = query.Provider.CreateQuery<Reservation>(resultExpression);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<long> CountReservationsWithStatusAsync(ReservationStatus status)
        {
            return await context
                .Reservations
                .Where(r => r.Status == status)
                .CountAsync();
        }

        public async Task<long> CountReservationsByStartMonthAsync(int month)
        {
            return await context
                .Reservations
                .Where(r => r.ReservationStart.Month == month)
                .CountAsync();
        }

        public async Task<List<ReservationCount>> GetReservationsWithCountsAsync()
        {
            return await context
                .Reservations
                .GroupBy(r => r.Instance.Resource.Title)
                .Select(res => new ReservationCount()
                {
                    Title = res.Key,
                    Count = res.Count()
                })
                .OrderByDescending(r => r.Count)
                .ToListAsync();
        }

        public async Task<List<Reservation>> FindAllWithStatusesAsync(List<ReservationStatus> statuses)
        {
            return await context
                .Reservations
                .Where(r => statuses.Contains(r.Status))
                .ToListAsync();
        }

        public async Task<List<MonthCount>> GetReservationCountPerMonthAsync()
        {
            return await context
                .Reservations
                .GroupBy(r => r.ReservationStart.Month)
                .Select(g => new MonthCount() { Month = g.Key, Count = g.Count()})
                .OrderByDescending(g => g.Month)
                .ToListAsync();
        }
    }
}
