using Microsoft.EntityFrameworkCore;
using Carteirinha.Models;


namespace Carteirinha.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
