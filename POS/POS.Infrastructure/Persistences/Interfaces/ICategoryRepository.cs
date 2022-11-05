using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> RemoveCategory2(int categoryId);
        Task<BaseEntityResponse<Category>> ListCategory(BaseFiltersRequest filters);
    }
}
