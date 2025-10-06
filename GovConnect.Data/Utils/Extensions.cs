using GovConnect.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GovConnect.Data.Entities;

namespace GovConnect.Data.Utils {
    public static class Extensions {
        public static EntityTypeBuilder<TEntity> ConfigureSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity: class {
            if (!typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity))) {
                return builder;
            }

            builder.HasQueryFilter(entity => !((ISoftDelete)entity).DeletedAt.HasValue);
            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureId<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class {
            builder.HasKey("Id");
                
            builder
                .Property("Id")
                .ValueGeneratedOnAdd();

            return builder;
        }
    }
}
