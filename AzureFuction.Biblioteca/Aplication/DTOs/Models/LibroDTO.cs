using Domain.Entidades;

namespace AzureFuction.Biblioteca.Aplication.DTOs.Models
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int EditorialId { get; set; }

        public Autor Autor { get; set; } = new Autor();
        public Editorial Editorial { get; set; } = new Editorial();
    }
}
