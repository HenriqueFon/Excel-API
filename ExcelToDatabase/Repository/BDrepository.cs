using ExcelToDatabase.Data;
using ExcelToDatabase.Models;
using ExcelToDatabase.Repository.Interfaces;
using ExcelToDatabase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExcelToDatabase.Repository
{
    public class BDrepository : IBDrepository
    {
        private readonly DBDataContext _dbDataContext;

        public BDrepository(DBDataContext dbDataContext)
        {
            _dbDataContext= dbDataContext;
        }

        public async Task<IEnumerable<Products>> getDataAsync()
        {
            var products = await _dbDataContext.products.ToListAsync();
            return products;
        }

        public async Task<IList<Products>> storageDataAsync(List<Products> excel)
        {
            //AddRange permite adicionar um array ou lista de objetos no bd
            _dbDataContext.products.AddRange(excel);

            await _dbDataContext.SaveChangesAsync();

            return excel;
        }
    }
}
