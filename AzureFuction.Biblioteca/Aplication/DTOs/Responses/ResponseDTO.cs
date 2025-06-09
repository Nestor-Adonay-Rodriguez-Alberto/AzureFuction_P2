using System;


namespace AzureFuction.Biblioteca.Aplication.DTOs.Responses
{
    public class ResponseDTO<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T? Data { get; set; }
    }
}
