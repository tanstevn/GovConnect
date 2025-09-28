using GovConnect.Data.Abstractions;
using GovConnect.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GovConnect.Data {
    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext() {
            base.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Barangay> Barangays { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<RequestAttachment> RequestAttchments { get; set; }
        public DbSet<RequestStatusHistory> RequestStatusHistories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentAttachment> CommentAttachments { get; set; }

        public override int SaveChanges() {
            SaveChangesInternally();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            SaveChangesInternally();
            return base.SaveChangesAsync(cancellationToken);
        }

        [Obsolete(@"
        This is to prevent to run multiple times the SaveChangesInternally. 
        Please off-limit yourself from calling this!")]
        public override int SaveChanges(bool acceptAllChangesOnSuccess) {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        [Obsolete(@"
        This is to prevent to run multiple times the SaveChangesInternally. 
        Please off-limit yourself from calling this!")]
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SaveChangesInternally() {
            var entries = base.ChangeTracker
                .Entries()
                .Where(entry => entry.State == EntityState.Deleted
                    && (entry.Metadata.BaseType is not null
                        ? typeof(ISoftDelete).IsAssignableFrom(entry.Metadata.BaseType.ClrType)
                        : entry.Entity is ISoftDelete)
                    && entry.Metadata.IsOwned()
                    && entry.Metadata is not EntityType {
                        IsImplicitlyCreatedJoinEntityType: true
                    });

            foreach (var scopedEntry in entries) {
                scopedEntry.Property(nameof(ISoftDelete.DeletedAt)).CurrentValue = DateTime.UtcNow;
                scopedEntry.State = EntityState.Modified;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
