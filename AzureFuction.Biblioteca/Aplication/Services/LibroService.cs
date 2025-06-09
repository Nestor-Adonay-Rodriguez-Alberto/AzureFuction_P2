using AzureFuction.Biblioteca.Aplication.DTOs.Models;
using AzureFuction.Biblioteca.Aplication.DTOs.Responses;
using DataAccess.Persistence.Tables;
using Domain.Entidades;
using Domain.Repositories;
using System;


namespace AzureFuction.Biblioteca.Aplication.Services
{
    public class LibroService
    {
        public ILibro _LibroRepository;

        public LibroService(ILibro _ILibro) 
        {
            _LibroRepository = _ILibro;
        }




        // SAVE - UPDATE:
        public async Task<ResponseDTO<LibroDTO>> SaveLibro(LibroDTO libroDTO)
        {
            Libro newLibro = MapToEntity(libroDTO);
            newLibro =  await _LibroRepository.SaveLibro(newLibro);
            LibroDTO libro = MapToDTO(newLibro);

            ResponseDTO<LibroDTO> responseDTO = new()
            {
                Message = libroDTO.Id > 0 ? "Se actualizo el Libro" : "Se Creo el Libro",
                Status = true,
                Data = libro
            };

            return responseDTO;
        }



        // DTO TO ENTITY:
        private Libro MapToEntity(LibroDTO libroDTO) 
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

        // ENTITY TO DTO:
        private LibroDTO MapToDTO(Libro libro)
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
    }
}
