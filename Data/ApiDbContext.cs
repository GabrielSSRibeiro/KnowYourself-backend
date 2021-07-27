using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<ItemData> Items { get; set;}

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options){}
    }
}