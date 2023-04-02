using ExcelToDatabase.Models;

namespace ExcelToDatabase.Repository.Interfaces
{
    public interface IBDrepository
    {
        public Task<IEnumerable<Products>> getDataAsync();
        public Task<IList<Products>> storageDataAsync(List<Products> excel);
    }
}
