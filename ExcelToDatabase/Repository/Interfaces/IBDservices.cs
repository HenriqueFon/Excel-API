using ExcelToDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using OfficeOpenXml;
using System.IO.MemoryMappedFiles;

namespace ExcelToDatabase.Repository.Interfaces
{
    public interface IBDservices
    {
        public Task<IEnumerable<Products>> getProductsAsync();
        public Task<IList<Products>> postProductAsync(IFormFile file);
        public Task<MemoryStream> generateExcelFile();
    }
}
