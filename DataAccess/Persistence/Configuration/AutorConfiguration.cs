using DataAccess.Persistence.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccess.Persistence.Configuration
{
    public class AutorConfiguration : IEntityTypeConfiguration<AutorTable>
    {
        public void Configure(EntityTypeBuilder<AutorTable> builder)
        {
            builder.ToTable("Autor", "dbo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("Id")
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Nombre)
                   .HasColumnName("Nombre")
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
