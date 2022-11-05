using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> AccountByUserName(string userName);
        Task<BaseEntityResponse<User>> ListUser(BaseFiltersRequest filters);
        Task<bool> RemoveUser2(int userId);

    }
}
