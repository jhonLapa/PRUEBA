using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;


namespace POS.Infrastructure.Persistences.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly POSContext _context;

        public ProductRepository(POSContext context) : base(context)
        {
            _context = context;
        }


        public async Task<BaseEntityResponse<Product>> ListProduct(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Product>();

            var product = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        product = product.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        product = product.Where(x => x.Code!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                product = product.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                product = product.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "Id";

            response.TotalRecords = await product.CountAsync();

            response.Items = await Ordering(filters, product, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<bool> RemoveProduct2(int productId)
        {
            var product = await _context.Products.AsNoTracking().DefaultIfEmpty().SingleOrDefaultAsync(p => p.Id.Equals(productId));
            _context.Remove(product);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }


    }
}
