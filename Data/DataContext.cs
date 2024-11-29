using Microsoft.EntityFrameworkCore;

namespace todoApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}

        public DbSet<User> Users => Set<User>();
        public DbSet<Note> Notes  => Set<Note>();
    }
}
