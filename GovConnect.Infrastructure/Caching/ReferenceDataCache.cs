using GovConnect.Data;
using GovConnect.Infrastructure.Abstractions.Caching;
using Microsoft.EntityFrameworkCore;

namespace GovConnect.Infrastructure.Caching {
    public sealed class ReferenceDataCache(ApplicationDbContext dbContext) : IReferenceDataCache {
        public IReadOnlySet<long> SubcategoryIds { get; private set; } = new HashSet<long>();
        public IReadOnlySet<long> BarangayIds { get; private set; } = new HashSet<long>();
        public IReadOnlySet<long> PriorityLevelIds { get; private set; } = new HashSet<long>();

        public async Task LoadAsync() {
            SubcategoryIds = new HashSet<long>(
                await dbContext
                    .Subcategories
                    .Select(subCategory => subCategory.Id)
                    .ToListAsync());

            BarangayIds = new HashSet<long>(
                await dbContext
                    .Barangays
                    .Select(barangay => barangay.Id)
                    .ToListAsync());

            PriorityLevelIds = new HashSet<long>(
                await dbContext
                    .PriorityLevels
                    .Select(priorityLevel => priorityLevel.Id)
                    .ToListAsync());
        }
    }
}
