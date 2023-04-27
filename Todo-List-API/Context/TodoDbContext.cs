using Microsoft.EntityFrameworkCore;
using Todo_List_API.Models;

namespace Todo_List_API.Context
{
    public class TodoDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Models.List> Lists { get; set; }
        public virtual DbSet<Models.Task> Tasks { get; set; }
        public virtual DbSet<ListCategory> ListCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySQL(Env.MYSQL_CONNECTION);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Username).IsRequired();
                entity.Property(x => x.Password).IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.HasOne(x => x.Owner).WithMany(x => x.Categories).HasForeignKey(x => x.OwnerId).HasPrincipalKey(x => x.Id);
            });

            modelBuilder.Entity<Models.List>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.HasOne(x => x.Owner).WithMany(x => x.CreatedLists).HasForeignKey(x => x.OwnerId).HasPrincipalKey(x => x.Id);
            });

            modelBuilder.Entity<Models.Task>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.List).WithMany(x => x.Tasks).HasForeignKey(x => x.ListId).HasPrincipalKey(x => x.Id);
                entity.HasOne(x => x.AssignedTo).WithMany(x => x.AssignedTasks).HasForeignKey(x => x.AssignedToId).HasPrincipalKey(x => x.Id);
                entity.HasOne(x => x.CreatedBy).WithMany(x => x.CreatedTasks).HasForeignKey(x => x.CreatedById).HasPrincipalKey(x => x.Id);
            });

            modelBuilder.Entity<ListCategory>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.UserId,
                    x.ListId
                });
                entity.HasOne(x => x.User).WithMany(x => x.AssignedLists).HasForeignKey(x => x.UserId).HasPrincipalKey(x => x.Id);
                entity.HasOne(x => x.List).WithMany(x => x.AssignedUsers).HasForeignKey(x => x.ListId).HasPrincipalKey(x => x.Id);
                entity.HasOne(x => x.Category).WithMany(x => x.ListCategories).HasForeignKey(x => x.CategoryId).HasPrincipalKey(x => x.Id);
            });
        }
    }
}
