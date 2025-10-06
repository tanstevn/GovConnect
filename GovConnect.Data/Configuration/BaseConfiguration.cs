using GovConnect.Data.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder) {
            var interfaces = typeof(TEntity)
                .GetInterfaces()
                .ToList();

            builder
                .ConfigureSoftDelete(interfaces)
                .ConfigureId(interfaces);
        }
    }
}
