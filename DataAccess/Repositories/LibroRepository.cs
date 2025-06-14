﻿using DataAccess.Persistence.Mappers;
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


        // GET:
        public async Task<Libro> GetById(int Id)
        {
            LibroTable? libroTable = await _DbContext.Libro.FirstOrDefaultAsync(l => l.Id == Id);
            
            if (libroTable == null)
            {
                throw new InvalidOperationException($"No se encontró el Libro con ID {Id}.");
            }

            return _mapper.MapToEntity(libroTable);
        }


        // DELETE:
        public async Task DeleteById(int id)
        {
            LibroTable? libroTable = await _DbContext.Libro.FirstOrDefaultAsync(l => l.Id == id);

            if (libroTable == null)
            {
                throw new InvalidOperationException($"No se encontró el Libro con ID {id}.");
            }

            _DbContext.Libro.Remove(libroTable);
            await _DbContext.SaveChangesAsync();
        }

        public Task<(int, List<Libro>)> GetAllLibros(string search, int page, int pageSize)
        {
            throw new NotImplementedException();
        }


    }
}
