using SIR.ERP.Infrastructure.FileStorage;

namespace POS.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
      // Declaracion o matricula de nuestros interfaces a niver de repository
      ICategoryRepository Category { get; }
      IUserRepository User { get; }
      IAzureStorage Storage { get; }
      IProductRepository Product { get; }
      ISaleRepository Sale { get; }


        void SaveChanges();
        Task SaveChangesAsync();

    }
}
