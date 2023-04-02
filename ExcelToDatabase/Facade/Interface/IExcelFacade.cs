using ExcelToDatabase.Models;

namespace ExcelToDatabase.Facade.Interface
{
    public interface IExcelFacade
    {
        public Task<MemoryStream> generateExcelFile();

        public Task<IEnumerable<Products>> getProductsAsync();

        public Task<IList<Products>> postProductAsync(IFormFile file);
    }
}
