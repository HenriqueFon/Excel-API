using ExcelToDatabase.Data;
using ExcelToDatabase.Models;
using ExcelToDatabase.Repository.Interfaces;
using ExcelToDatabase.Services;
using ExcelToDatabase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ExcelToDatabase.Repository
{
    public class BDservices : IBDservices
    {
        private readonly DBDataContext _dbDataContext;
        private readonly IExcelInterface _excel;

        public BDservices(DBDataContext dbDataContext, IExcelInterface excel)
        {
            _dbDataContext= dbDataContext;
            _excel= excel;
        }

        public async Task<MemoryStream> generateExcelFile()
        {
            var data = await getProductsAsync();
            var excel = _excel.CreateStream(data);

            using (var memoryStream = new MemoryStream())
            {
                excel.SaveAs(memoryStream);
                var content = memoryStream.ToArray();
                return new MemoryStream(content);
            }
        }

        public async Task<IEnumerable<Products>> getProductsAsync()
        {
            var products = await _dbDataContext.products.ToListAsync();
            return products;
        }

        public async Task<IList<Products>> postProductAsync(IFormFile file)
        {
            var stream = _excel.ReadStream(file);
            var excel = _excel.ReadXls(stream);

            //AddRange permite adicionar um array ou lista de objetos no bd
            _dbDataContext.products.AddRange(excel);

            await _dbDataContext.SaveChangesAsync();

            return excel;
        }
    }
}
