using DataEdge_CustomerService.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataEdgeCustomerService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataBaseImportController : Controller
    {
        private readonly DataBaseImportService _dataBaseImportService;

        public DataBaseImportController( DataBaseImportService dataBaseImportService)
        {
            _dataBaseImportService = dataBaseImportService;
        }

        [HttpGet("Import")]
        public async Task<IActionResult> Import()
        {
            var response = await _dataBaseImportService.Execute();

            return Ok(response);
        }
    }
}
