using ExcelToDatabase.Facade.Interface;
using ExcelToDatabase.Models;
using ExcelToDatabase.Repository.Interfaces;
using ExcelToDatabase.Services.Interfaces;

namespace ExcelToDatabase.Facade
{
    public class ExcelFacade : IExcelFacade
    {
        private readonly IReadStream _stream;
        private readonly IExcelInterface _excel;
        private readonly IBDrepository _db;

        public ExcelFacade(IReadStream stream, IExcelInterface excel, IBDrepository db) {
            _stream = stream;
            _excel = excel;
            _db = db;
        }

        public async Task<MemoryStream> generateExcelFile()
        {
            var db = await _db.getDataAsync();
            var excel = _excel.CreateExcelFile(db);

            return excel;
        }

        public async Task<IEnumerable<Products>> getProductsAsync()
        {
            return await _db.getDataAsync();
        }

        public async Task<IList<Products>> postProductAsync(IFormFile file)
        {
            var stream = _stream.CreateStream(file);
            var excel = _excel.ReadXls(stream);
            var db = await _db.storageDataAsync(excel);

            return db;
        }
    }
}
