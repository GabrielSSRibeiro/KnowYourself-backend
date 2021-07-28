using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<SignData> Signs { get; set;}
        public virtual DbSet<GenerationData> Generations { get; set;}

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options){}
    }
}