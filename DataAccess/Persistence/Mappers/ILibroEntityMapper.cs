using DataAccess.Persistence.Tables;
using Domain.Entidades;


namespace DataAccess.Persistence.Mappers
{
    public interface ILibroEntityMapper
    {
        Libro MapToEntity(LibroTable table);
        LibroTable MapToTable(Libro entity);
    }
}
