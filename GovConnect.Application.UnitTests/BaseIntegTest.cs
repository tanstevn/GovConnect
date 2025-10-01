using GovConnect.Data;
using Microsoft.EntityFrameworkCore;

namespace GovConnect.Application.IntegrationTests {
    public abstract class BaseIntegTest {
        protected ApplicationDbContext DbContext { get; private set; }

        protected BaseIntegTest() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            DbContext = new ApplicationDbContext(options);
        }
    }
}
