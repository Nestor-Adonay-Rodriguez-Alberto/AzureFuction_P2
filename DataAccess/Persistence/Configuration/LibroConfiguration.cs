using DataAccess.Persistence.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Persistence.Configuration
{
    public class LibroConfiguration : IEntityTypeConfiguration<LibroTable>
    {
        public void Configure(EntityTypeBuilder<LibroTable> builder)
        {
            builder.ToTable("Libro", "dbo");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Titulo)
                   .HasColumnName("Titulo")
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.AutorId)
                   .HasColumnName("AutorId")
                   .IsRequired();

            builder.Property(e => e.EditorialId)
                   .HasColumnName("EditorialId")
                   .IsRequired();

            // RELACION: Un libro tiene un autor
            builder.HasOne(e => e.Autor)
                   .WithMany(a => a.Libros)
                   .HasForeignKey(e => e.AutorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // RELACION: Un libro tiene una editorial
            builder.HasOne(e => e.Editorial)
                   .WithMany(ed => ed.Libros)
                   .HasForeignKey(e => e.EditorialId)
                   .OnDelete(DeleteBehavior.Restrict);

            // CARGAR LOS OBJ:
            builder.Navigation(e => e.Autor).AutoInclude();
            builder.Navigation(e => e.Editorial).AutoInclude();
        }
    }
}
