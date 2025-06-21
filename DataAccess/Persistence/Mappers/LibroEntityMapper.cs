using DataAccess.Persistence.Tables;
using Domain.Entidades;


namespace DataAccess.Persistence.Mappers
{
    public class LibroEntityMapper : ILibroEntityMapper
    {
        // TABLE TO ENTITY:
        public Libro MapToEntity(LibroTable libroTable)
        {
            Libro libro = new()
            {
                Id = libroTable.Id,
                Titulo = libroTable.Titulo,
                AutorId = libroTable.AutorId,
                EditorialId = libroTable.EditorialId
            };

            if (libroTable.Autor != null)
            {
                libro.Autor = new()
                {
                    Id = libroTable.Autor.Id,
                    Nombre = libroTable.Autor.Nombre
                };
            }

            if (libroTable.Editorial != null)
            {
                libro.Editorial = new()
                {
                    Id = libroTable.Editorial.Id,
                    Nombre = libroTable.Editorial.Nombre
                };
            }

            return libro;
        }

        // ENTITY TO TABLE:
        public LibroTable MapToTable(Libro newLibro)
        {
            LibroTable libroTable = new()
            {
                Id = newLibro.Id,
                Titulo = newLibro.Titulo,
                AutorId = newLibro.AutorId,
                EditorialId = newLibro.EditorialId,
            };

            return libroTable;
        }

        // LIST TABLE TO LIST ENTITY:
        public List<Libro> MapToEntityList(List<LibroTable> libroTables)
        {
            if (libroTables == null) return new List<Libro>();

            List<Libro> libros = [];

            foreach (LibroTable lt in libroTables)
            {
                Libro libro = new()
                {
                    Id = lt.Id,
                    Titulo = lt.Titulo,
                    AutorId = lt.AutorId,
                    EditorialId = lt.EditorialId
                };

                if (lt.Autor != null)
                {
                    libro.Autor = new Autor
                    {
                        Id = lt.Autor.Id,
                        Nombre = lt.Autor.Nombre
                    };
                }

                if (lt.Editorial != null)
                {
                    libro.Editorial = new Editorial
                    {
                        Id = lt.Editorial.Id,
                        Nombre = lt.Editorial.Nombre
                    };
                }

                libros.Add(libro);
            }

            return libros;
        }

    }
}
