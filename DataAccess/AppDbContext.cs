using DataAccess.Persistence.Configuration;
using DataAccess.Persistence.Tables;
using Microsoft.EntityFrameworkCore;


namespace DataAccess
{
    public class AppDbContext: DbContext
    {

        // CONSTRUCTOR:
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        {

        } 

        // TABLAS EN DB:
        public DbSet<AutorTable> Autor { get; set; }
        public DbSet<EditorialTable> Editorial { get; set; }
        public DbSet<LibroTable> Libro { get; set; }

        // CONFIGURACION:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // T-Autor
            modelBuilder.ApplyConfiguration(new AutorConfiguration());

            // T-Editorial
            modelBuilder.ApplyConfiguration(new EditorialConfiguration());

            // T-Libro
            modelBuilder.ApplyConfiguration(new LibroConfiguration());
        }

    }
}
