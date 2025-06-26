using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoApp.Models;

namespace TodoApp.AppDataContext
{
    public class TodoDbContext :DbContext
    {
        private readonly DbSettings _dbsettings;

        public TodoDbContext(IOptions<DbSettings> dsSettings)
        {
            _dbsettings = dsSettings.Value;
        }

        public DbSet<Todo> Todos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbsettings.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("TodoAPI").HasKey(t => t.Id);
        }
    }
}
