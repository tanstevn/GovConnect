using GovConnect.Data;
using Microsoft.EntityFrameworkCore;

namespace GovConnect.Application.IntegrationTests {
    public abstract class BaseIntegTest {
        protected ApplicationDbContext DbContext { get; private set; }

        protected BaseIntegTest() {
            var databaseName = Guid
                .NewGuid()
                .ToString();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            DbContext = new ApplicationDbContext(options);
        }
    }
}
