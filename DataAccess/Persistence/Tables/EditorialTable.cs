using System;


namespace DataAccess.Persistence.Tables
{
    public class EditorialTable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<LibroTable> Libros { get; set; } = new List<LibroTable>();
    }
}
