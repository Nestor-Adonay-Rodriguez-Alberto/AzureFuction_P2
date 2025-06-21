using AzureFuction.Biblioteca.Aplication.DTOs.Models;
using AzureFuction.Biblioteca.Aplication.DTOs.Responses;
using AzureFuction.Biblioteca.Aplication.Services;
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


        // GET:
        [Function("fn-get-libro-id")]
        public async Task<IActionResult> GetById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "azure-fuction/get-libro/{Id}")] HttpRequest req, int Id)
        {
            _Logger.LogInformation("Obteniendo El Libro...");

            try
            {
                ResponseDTO<LibroDTO> response = await _service.GetById(Id);
                return new OkObjectResult(response);
            }
            catch (InvalidOperationException ex)
            {
                _Logger.LogError($"Error al obtener el Libro: {ex.Message}");
                return new NotFoundObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error inesperado al obtener el empleado: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        // DELETE:
        [Function("fn-delete-libro")]
        public async Task<IActionResult> DeleteById([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "azure-fuction/delete-libro/{Id}")] HttpRequest req, int Id)
        {
            _Logger.LogInformation("Eliminando El Libro...");

            try
            {
                ResponseDTO<LibroDTO> response = await _service.DeleteById(Id);
                return new OkObjectResult(response);
            }
            catch (InvalidOperationException ex)
            {
                _Logger.LogError($"Error al eliminar el Libro: {ex.Message}");
                return new NotFoundObjectResult(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error inesperado al obtener el empleado: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        // GET ALL:
        [Function("fn-get-all-libros")]
        public async Task<IActionResult> GetAllLibros([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "azure-fuction/get-all-libros")] HttpRequest req) 
        {
            _Logger.LogInformation("Obteniendo Todos Los Libros...");
            try
            {
                string search = req.Query.ContainsKey("search") ? req.Query["search"].ToString() : string.Empty;
                int page = 1;
                int pageSize = 20;


                if (req.Query.ContainsKey("page") && int.TryParse(req.Query["page"], out int parsedPage))
                {
                    page = parsedPage;
                }

                if (req.Query.ContainsKey("pageSize") && int.TryParse(req.Query["pageSize"], out int parsedPageSize))
                {
                    pageSize = parsedPageSize;
                }          

                var (totalCount, listItems) = await _service.GetAllLibros(search, page, pageSize);

                PaginatedResponseDTO<List<LibrosListDTO>> response =new()
                {
                    Message = "Lobrios Obtenidos Exitosamente",
                    Status = true,
                    Data = listItems,
                    Count = totalCount,
                    CurrentPage = page,
                    PageSize = pageSize
                };

                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _Logger.LogError($"Error inesperado al listar los Libros: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
   
    
    }
}
