using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.Identity.Client;

namespace Checks_Racks.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Line> Lines { get; set; } = null!;
        public DbSet<Computer> Computers { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public virtual void AddOrUpdate<T>(ApplicationContext db, T entity) where T : Data
        {
            var isExists = db.Set<T>().Any(x => x.Name == entity.Name);

            if (isExists)
                db.Set<T>().Update(entity);
            else
                db.Set<T>().Add(entity);
        }
    }
}
