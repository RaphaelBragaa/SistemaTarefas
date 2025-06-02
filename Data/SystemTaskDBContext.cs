using Microsoft.EntityFrameworkCore;
using SistemaTarefa.Data.Map;
using SistemaTarefa.Models;

namespace SistemaTarefa.Data
{
    public class SystemTaskDBContext : DbContext
    {
        public SystemTaskDBContext(DbContextOptions<SystemTaskDBContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TaskMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}

