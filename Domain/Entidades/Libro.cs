﻿using System;

namespace Domain.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int EditorialId { get; set; }

        public Autor Autor { get; set; }
        public Editorial Editorial { get; set; }
    }
}
