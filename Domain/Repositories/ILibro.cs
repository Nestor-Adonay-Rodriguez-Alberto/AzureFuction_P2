using Domain.Entidades;

namespace Domain.Repositories
{
    public interface ILibro
    {
        Task<Libro> SaveLibro(Libro libro);
        Task DeleteById(int id);
        Task<Libro> GetById(int Id);
        Task<(int, List<Libro>)> GetAllLibros(string search, int page, int pageSize);
    }
}
