

using AzureFuction.Biblioteca.Aplication.DTOs.Models;
using AzureFuction.Biblioteca.Aplication.DTOs.Responses;
using AzureFuction.Biblioteca.Aplication.Mappers;
using AzureFuction.Biblioteca.Aplication.Services;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFuction.Biblioteca.Controllers
{
    public class FnLibro
    {
        private readonly ILogger<FnLibro> _Logger;
        private readonly ILibroService _service;


        public FnLibro(ILogger<FnLibro> logger, ILibroService service)
        {
            _Logger = logger;
            _service = service;
        }


        // SAVE - UPDATE:
        [Function("fn-save-libro")]
        public async Task<IActionResult> SaveLibro([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "azure-fuction/save-libro")] HttpRequest req)
        {
            _Logger.LogInformation("Saving El Libro...");


            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var newLibro = JsonConvert.DeserializeObject<LibroDTO>(requestBody);

                ResponseDTO<LibroDTO> response = await _service.SaveLibro(newLibro);
                return new CreatedResult("Creacion Exitosa", response);
            }
            catch (InvalidOperationException ex)
            {
                _Logger.LogError($"Error de operación: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error inesperado al crear el libro: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
