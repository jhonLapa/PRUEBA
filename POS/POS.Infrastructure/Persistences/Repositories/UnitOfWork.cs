using Microsoft.Extensions.Configuration;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;
using SIR.ERP.Infrastructure.FileStorage;

namespace POS.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSContext _context;

        public ICategoryRepository Category { get; private set;   }
        public IUserRepository User { get; private set; }
        public IAzureStorage Storage { get; private set; }
        public IProductRepository Product { get; private set; }
        public ISaleRepository Sale { get; private set; }


        public UnitOfWork(POSContext context , IConfiguration configuration)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
            Product = new ProductRepository(_context);
            Storage = new AzureStorage(configuration);
            Sale = new SaleRepository(_context);


        }

        public void Dispose()
        {
           _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

    
    }
}
