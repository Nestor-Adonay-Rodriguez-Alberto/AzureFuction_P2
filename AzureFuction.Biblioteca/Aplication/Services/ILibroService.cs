using AzureFuction.Biblioteca.Aplication.DTOs.Models;
using AzureFuction.Biblioteca.Aplication.DTOs.Responses;
using Domain.Entidades;


namespace AzureFuction.Biblioteca.Aplication.Services
{
    public interface ILibroService
    {
        Task<ResponseDTO<LibroDTO>> SaveLibro(LibroDTO libroDTO);
        Task<ResponseDTO<LibroDTO>> GetById(int Id);
    }
}
