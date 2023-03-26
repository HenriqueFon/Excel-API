using ExcelDataReader;
using ExcelToDatabase.Models;
using ExcelToDatabase.Repository.Interfaces;
using ExcelToDatabase.Services;
using ExcelToDatabase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ExcelToDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcellController : ControllerBase
    {
        private readonly IBDservices _bd;

        public ExcellController(IBDservices bd)
        {
            _bd = bd;
        }

        [HttpGet("Get data")]
        public async Task<ActionResult<IEnumerable<Products>>> GetData()
        {
            var products = await _bd.getProductsAsync();
            return Ok(products);
        }

        
        public async Task<ActionResult<FileResult>> DowloadExcel()
        {
            var excel = _bd.generateExcelFile();
            var file = new FileInfo(excel);
        }

        //Expera um multipart/form-data seja passado nesse endpoint
        [Consumes("multipart/form-data")]
        [HttpPost("Insert Data")]
        public async Task<ActionResult<Products>> InputFile(IFormFile file)
        {
            var saveExcel = await _bd.postProductAsync(file);
            return Ok(saveExcel);
        }
    }
}