using AzureFuction.Biblioteca.Aplication.DTOs.Models;
using AzureFuction.Biblioteca.Aplication.DTOs.Responses;


namespace AzureFuction.Biblioteca.Aplication.Services
{
    public interface ILibroService
    {
        Task<ResponseDTO<LibroDTO>> SaveLibro(LibroDTO libroDTO);
        Task<ResponseDTO<LibroDTO>> GetById(int Id);
        Task<ResponseDTO<LibroDTO>> DeleteById(int Id);
    }
}
