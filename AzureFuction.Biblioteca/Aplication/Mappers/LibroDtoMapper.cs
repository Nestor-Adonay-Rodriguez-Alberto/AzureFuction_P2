using AzureFuction.Biblioteca.Aplication.DTOs.Models;
using Domain.Entidades;


namespace AzureFuction.Biblioteca.Aplication.Mappers
{
    public class LibroDtoMapper : ILibroDtoMapper
    {

        // ENTITY TO DTO:
        public LibroDTO MapToDTO(Libro libro)
        {
            LibroDTO libroDTO = new()
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                AutorId = libro.AutorId,
                EditorialId = libro.EditorialId,
            };

            if (libro.Autor != null)
            {
                libroDTO.Autor = new()
                {
                    Id = libro.Autor.Id,
                    Nombre = libro.Autor.Nombre
                };
            }

            if (libro.Editorial != null)
            {
                libroDTO.Editorial = new()
                {
                    Id = libro.Editorial.Id,
                    Nombre = libro.Editorial.Nombre
                };
            }

            return libroDTO;
        }


        // DTO TO ENTITY:
        public Libro MapToEntity(LibroDTO libroDTO)
        {
            Libro libro = new()
            {
                Id = libroDTO.Id,
                Titulo = libroDTO.Titulo,
                AutorId = libroDTO.AutorId,
                EditorialId = libroDTO.EditorialId,
            };

            return libro;
        }

    }
}
