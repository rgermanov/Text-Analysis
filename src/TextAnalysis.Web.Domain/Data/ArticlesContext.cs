using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TextAnalysis.Web.Domain.Models;

namespace TextAnalysis.Web.Domain.Data 
{
    public class ArticlesContext : DbContext
    {
        public ArticlesContext(DbContextOptions<ArticlesContext> options) :
            base(options)
        {}
        
        public DbSet<Article> Articles { get; set; }
    }
}