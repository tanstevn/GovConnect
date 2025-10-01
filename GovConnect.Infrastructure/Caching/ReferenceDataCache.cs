using GovConnect.Data;
using GovConnect.Infrastructure.Abstractions.Caching;
using Microsoft.EntityFrameworkCore;

namespace GovConnect.Infrastructure.Caching {
    public sealed class ReferenceDataCache : IReferenceDataCache {
        private readonly ApplicationDbContext _dbContext;

        public IReadOnlySet<long> SubcategoryIds { get; private set; } = new HashSet<long>();
        public IReadOnlySet<long> BarangayIds { get; private set; } = new HashSet<long>();
        public IReadOnlySet<long> PriorityLevelIds { get; private set; } = new HashSet<long>();

        public ReferenceDataCache(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task LoadAsync() {
            SubcategoryIds = new HashSet<long>(
                await _dbContext
                    .Subcategories
                    .Select(subCategory => subCategory.Id)
                    .ToListAsync());

            BarangayIds = new HashSet<long>(
                await _dbContext
                    .Barangays
                    .Select(barangay => barangay.Id)
                    .ToListAsync());

            PriorityLevelIds = new HashSet<long>(
                await _dbContext
                    .PriorityLevels
                    .Select(priorityLevel => priorityLevel.Id)
                    .ToListAsync());
        }
    }
}
