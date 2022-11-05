using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;


namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<BaseEntityResponse<Product>> ListProduct(BaseFiltersRequest filters);
        Task<bool> RemoveProduct2(int productId);

    }
}