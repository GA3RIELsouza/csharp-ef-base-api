using Base_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Base_API.Infrastructure.Context
{
    public class MainDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            Database.AutoSavepointsEnabled = false;
            Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.IsAdmin)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }

        public void ExecuteInTrasaction(Action query)
        {
            try
            {
                Database.BeginTransaction();

                query();

                SaveChanges();
                Database.CommitTransaction();
            }
            catch (Exception)
            {
                Database.RollbackTransaction();
                throw;
            }
        }

        public async Task ExecuteInTrasactionAsync(Func<Task> query)
        {
            try
            {
                await Database.BeginTransactionAsync();

                await query();

                await SaveChangesAsync();
                await Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await Database.RollbackTransactionAsync();
                throw;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetEntitiesInsertedUpdated();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetEntitiesInsertedUpdated();

            return base.SaveChanges();
        }

        private void SetEntitiesInsertedUpdated()
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                var entity = entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.SetInsertedNow();
                        break;

                    case EntityState.Modified:
                        entity.SetUpdatedNow();
                        break;
                }
            }
        }
    }
}
