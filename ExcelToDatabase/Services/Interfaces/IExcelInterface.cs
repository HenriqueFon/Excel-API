using ExcelToDatabase.Models;
using OfficeOpenXml;

namespace ExcelToDatabase.Services.Interfaces
{
    public interface IExcelInterface
    {
        public List<Products> ReadXls(MemoryStream file);

        public MemoryStream ReadStream(IFormFile file);

        public ExcelPackage CreateStream(IEnumerable<Products> data);
    }
}
