using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly POSContext _context;

        public CategoryRepository(POSContext context) : base(context)
        {
            _context = context;
        }


        public async Task<BaseEntityResponse<Category>> ListCategory(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Category>();

            //   var category = (from c in _context.Categories
            //    where c.AuditDeleteUser == null && c.AuditDeleteDate == null
            //    select c).AsNoTracking().AsQueryable();

            var category = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        category = category.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        category = category.Where(x => x.Description!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                category = category.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                category = category.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await category.CountAsync();

            response.Items = await Ordering(filters, category, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<bool> RemoveCategory2(int categoryId)
        {
            var category = await _context.Categories.AsNoTracking().DefaultIfEmpty().SingleOrDefaultAsync(c => c.Id.Equals(categoryId));
            _context.Remove(category);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
    }
}
