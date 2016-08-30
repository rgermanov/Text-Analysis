using Microsoft.EntityFrameworkCore;
using TextAnalysis.Web.Domain.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace TextAnalysis.Web.Domain.Data
{
    public class ResourcesContext : DbContext
    {
        public ResourcesContext(DbContextOptions<ResourcesContext> options) :
            base(options)
        {}
        
        public DbSet<ResourceContent> Resources { get; set; }

        public DbSet<ResourceUrl> ResourceUrls { get; set; }
        
        protected override void OnModelCreating(ModelBuilder  modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}