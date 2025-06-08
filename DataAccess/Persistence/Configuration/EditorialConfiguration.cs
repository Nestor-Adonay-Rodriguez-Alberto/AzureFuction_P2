using DataAccess.Persistence.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccess.Persistence.Configuration
{
    public class EditorialConfiguration : IEntityTypeConfiguration<EditorialTable>
    {
        public void Configure(EntityTypeBuilder<EditorialTable> builder)
        {
            builder.ToTable("Editorial", "dbo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Nombre)
                   .HasColumnName("Nombre")
                   .IsRequired()
                   .HasMaxLength(100);

            // RELACION: Una editoria tiene muchos libros
            builder.HasMany(e => e.Libros)
                   .WithOne(e => e.Editorial)
                   .HasForeignKey(e => e.EditorialId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
