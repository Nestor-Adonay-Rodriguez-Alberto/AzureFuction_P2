using System;


namespace DataAccess.Persistence.Tables
{
    public class LibroTable
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int EditorialId { get; set; }

        public AutorTable Autor { get; set; } = new AutorTable();
        public EditorialTable Editorial { get; set; } = new EditorialTable();
    }
}
