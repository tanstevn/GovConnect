using GovConnect.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Utils {
    public static class Extensions {
        public static EntityTypeBuilder<TEntity> ConfigureSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> builder, IEnumerable<Type> types)
            where TEntity: class {
            if (!types.Contains(typeof(ISoftDelete))) {
                return builder;
            }

            builder.HasQueryFilter(entity => !((ISoftDelete)entity).DeletedAt.HasValue);
            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureId<TEntity>(this EntityTypeBuilder<TEntity> builder, IEnumerable<Type> types)
            where TEntity : class {
            builder.HasKey("Id");

            builder
                .Property("Id")
                .ValueGeneratedOnAdd();

            return builder;
        }
    }
}
