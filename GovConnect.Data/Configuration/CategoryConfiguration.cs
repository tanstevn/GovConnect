using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class CategoryConfiguration : BaseConfiguration<Category> {
        public override void Configure(EntityTypeBuilder<Category> builder) {
            builder
                .Property(entity => entity.Name)
                .IsRequired();

            builder
                .HasMany(entity => entity.Subcategories)
                .WithOne(subCategory => subCategory.Category);

            base.Configure(builder);
        }
    }
}
