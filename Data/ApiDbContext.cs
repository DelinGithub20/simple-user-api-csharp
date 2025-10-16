using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;

namespace SimpleApi.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {}
    }
}