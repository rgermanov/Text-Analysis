using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TextAnalysis.Web.Domain.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace TextAnalysis.Web.Domain.Data 
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext(DbContextOptions<ArticlesContext> options) :
            base(options)
        {}
        
        public DbSet<Article> Articles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder  modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            
            base.OnModelCreating(modelBuilder);
        }
    }
}