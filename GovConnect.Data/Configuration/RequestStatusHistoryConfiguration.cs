using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovConnect.Data.Configuration {
    public class RequestStatusHistoryConfiguration : BaseConfiguration<RequestStatusHistory> {
        public override void Configure(EntityTypeBuilder<RequestStatusHistory> builder) {
            builder
                .HasOne(entity => entity.Request)
                .WithMany(request => request.RequestStatusHistories)
                .HasForeignKey(history => history.RequestId)
                .IsRequired();

            builder
                .HasOne(entity => entity.Status)
                .WithOne(status => status.RequestStatusHistory)
                .HasForeignKey<RequestStatusHistory>(history => history.StatusId)
                .IsRequired();

            builder
                .HasOne(entity => entity.User)
                .WithOne(user => user.RequestStatusHistory)
                .HasForeignKey<RequestStatusHistory>(history => history.UpdatedBy)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
