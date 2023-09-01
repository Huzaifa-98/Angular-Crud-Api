using CRUDApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUDApi.Data
{
    public class CrudApiDbContext : DbContext
    {
        public CrudApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
