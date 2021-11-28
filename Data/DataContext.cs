using System.IO;
using Filme.Data.Map;
using Microsoft.EntityFrameworkCore;
using Filmes.Models;
using Microsoft.Extensions.Configuration;

namespace Filme.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            
        }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("FilmesDb");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new VideoMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
        }
    }
}
