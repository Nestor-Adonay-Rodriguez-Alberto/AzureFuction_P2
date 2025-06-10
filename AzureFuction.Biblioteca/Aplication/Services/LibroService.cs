using AzureFuction.Biblioteca.Aplication.DTOs.Models;
using AzureFuction.Biblioteca.Aplication.DTOs.Responses;
using AzureFuction.Biblioteca.Aplication.Mappers;
using DataAccess.Persistence.Tables;
using Domain.Entidades;
using Domain.Repositories;


namespace AzureFuction.Biblioteca.Aplication.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibro _LibroRepository;
        private readonly ILibroDtoMapper _mapper;


        public LibroService(ILibro _ILibro, ILibroDtoMapper mapper) 
        {
            _LibroRepository = _ILibro;
            _mapper = mapper;
        }




        // SAVE - UPDATE:
        public async Task<ResponseDTO<LibroDTO>> SaveLibro(LibroDTO libroDTO)
        {
            Libro newLibro = _mapper.MapToEntity(libroDTO);
            newLibro =  await _LibroRepository.SaveLibro(newLibro);
            LibroDTO libro = _mapper.MapToDTO(newLibro);

            ResponseDTO<LibroDTO> responseDTO = new()
            {
                Message = libroDTO.Id > 0 ? "Se actualizo el Libro" : "Se Creo el Libro",
                Status = true,
                Data = libro
            };

            return responseDTO;
        }


        // GET:
        public async Task<ResponseDTO<LibroDTO>> GetById(int Id)
        {
            Libro libro = await _LibroRepository.GetById(Id);
            LibroDTO libroDTO = _mapper.MapToDTO(libro);

            ResponseDTO<LibroDTO> responseDTO = new()
            {
                Message = "Libro Obtenido...",
                Status = true,
                Data = libroDTO
            };

            return responseDTO;
        }


        // DELETE:
        public async Task<ResponseDTO<LibroDTO>> DeleteById(int Id)
        {
            await _LibroRepository.DeleteById(Id);

            ResponseDTO<LibroDTO> responseDTO = new()
            {
                Message = "Libro Eliminado Con Exito.",
                Status = true,
                Data = null
                
            };

            return responseDTO;
        }

    }
}
