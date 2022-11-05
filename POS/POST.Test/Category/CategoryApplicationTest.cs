using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POST.Test.Category
{
    [TestClass]
    public class CategoryApplicationTest
    {
        private static WebApplicationFactory<Program> _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        [ClassInitialize]
        public static void Initialize (TestContext testContext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        }


    }
}
