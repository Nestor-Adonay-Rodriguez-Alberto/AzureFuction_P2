using AzureFuction.Biblioteca.Aplication.DTOs.Models;
using Domain.Entidades;


namespace AzureFuction.Biblioteca.Aplication.Mappers
{
    public interface ILibroDtoMapper
    {
        Libro MapToEntity(LibroDTO dto);
        LibroDTO MapToDTO(Libro entity);
        List<LibrosListDTO> MapToListDTO(List<Libro> libros);
    }
}
