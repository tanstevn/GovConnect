using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class SubcategoryConfiguration : BaseConfiguration<Subcategory> {
        public override void Configure(EntityTypeBuilder<Subcategory> builder) {
            builder
                .Property(entity => entity.Name)
                .IsRequired();

            builder
                .HasOne(entity => entity.Category)
                .WithMany(category => category.Subcategories)
                .HasForeignKey(subCategory => subCategory.CategoryId)
                .IsRequired();

            builder
                .HasMany(entity => entity.Requests)
                .WithOne(request => request.Subcategory);

            base.Configure(builder);
        }
    }
}
