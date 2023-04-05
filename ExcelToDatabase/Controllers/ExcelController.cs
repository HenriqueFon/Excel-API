using ExcelToDatabase.Facade.Interface;
using ExcelToDatabase.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExcelToDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelFacade _excelFacade;

        public ExcelController(IExcelFacade excelFacade)
        {
            _excelFacade = excelFacade;
        }

        [HttpGet("Get-data")]
        public async Task<ActionResult<IEnumerable<Products>>> GetData()
        {
            var products = await _excelFacade.getProductsAsync();
            return Ok(products);
        }

        [HttpGet("Get-file")]
        public async Task<IActionResult> DownloadExcel()
        {
            var excel = _excelFacade.generateExcelFile();
            
            return File(excel.Result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Planilha.xlsx");
        }

        [HttpGet("Get-model-file")]
        public async Task<IActionResult> DownloadExcelModel()
        {
            var excel = _excelFacade.generateExcelModelFile();

            return File(excel.Result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Planilha.xlsx");
        }

        //Espera um multipart/form-data seja passado nesse endpoint
        [Consumes("multipart/form-data")]
        [HttpPost("Insert-Data")]
        public async Task<ActionResult<Products>> InputFile(IFormFile file)
        {
            var saveExcel = await _excelFacade.postProductAsync(file);
            return Ok(saveExcel);
        }
    }
}