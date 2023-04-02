using ExcelToDatabase.Models;

namespace ExcelToDatabase.Services.Interfaces
{
    public interface IExcelInterface
    {
        public List<Products> ReadXls(MemoryStream file);

        public MemoryStream CreateExcelFile(IEnumerable<Products> data);
    }
}
