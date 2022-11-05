using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly POSContext _context;

        public UserRepository(POSContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> AccountByUserName(string userName)
        {
            var account = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName!.Equals(userName));
            return account!;
        }

        public async Task<BaseEntityResponse<User>> ListUser(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<User>();

            //   var category = (from c in _context.Categories
            //    where c.AuditDeleteUser == null && c.AuditDeleteDate == null
            //    select c).AsNoTracking().AsQueryable();

            var User = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        User = User.Where(x => x.UserName!.Contains(filters.TextFilter));
                        break;

                }
            }

            if (filters.StateFilter is not null)
            {
                User = User.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                User = User.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await User.CountAsync();

            response.Items = await Ordering(filters, User, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<bool> RemoveUser2(int userId)
        {
            var user = await _context.Users.AsNoTracking().DefaultIfEmpty().SingleOrDefaultAsync(u => u.Id.Equals(userId));
            _context.Remove(user);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
    }
}
