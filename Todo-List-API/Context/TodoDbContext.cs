using Microsoft.EntityFrameworkCore;
using Todo_List_API.Models;

namespace Todo_List_API.Context
{
    public class TodoDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

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
                entity.HasOne(x => x.Owner).WithMany(x => x.Categories).HasForeignKey(x => x.Id);
            });
        }
    }
}
