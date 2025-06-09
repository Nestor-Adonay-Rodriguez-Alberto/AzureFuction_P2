

using DataAccess.Persistence.Mappers;
using DataAccess.Persistence.Tables;
using Domain.Entidades;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class LibroRepository : ILibro
    {
        private readonly AppDbContext _DbContext;
        private readonly ILibroEntityMapper _mapper;

        public LibroRepository(AppDbContext dbContext, ILibroEntityMapper mappe)
        {
            _DbContext = dbContext;
            _mapper = mappe;
        }




        // SAVE - UPDATE:
        public async Task<Libro> SaveLibro(Libro newLibro)
        {
            using var transaction = await _DbContext.Database.BeginTransactionAsync();
            try
            {
                LibroTable? libroTable;

                if (newLibro.Id == 0)
                {
                    libroTable = _mapper.MapToTable(newLibro);
                    _DbContext.Libro.Add(libroTable);
                    await _DbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                else
                {
                    libroTable = await _DbContext.Libro.FirstOrDefaultAsync(x => x.Id == newLibro.Id);
                    if (libroTable == null)
                    {
                        throw new InvalidOperationException($"No se encontró el LIBRO con ID {newLibro.Id} para actualizar.");
                    }

                    _DbContext.Entry(libroTable).CurrentValues.SetValues(newLibro);

                    await _DbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }

                LibroTable? libro = await _DbContext.Libro.FirstOrDefaultAsync(x => x.Id == libroTable.Id);
                return _mapper.MapToEntity(libro!);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Error al querer actualizar el Libro", ex);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Error inesperado al procesar la operación.", ex);
            }
        }


        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<(int, List<Libro>)> GetAllLibros(string search, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Libro> GetById(int Id)
        {
            throw new NotImplementedException();
        }



        // TABLE TO ENTITY:
        private Libro MapToEntity(LibroTable libroTable)
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

    }
}
