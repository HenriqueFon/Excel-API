using ExcelToDatabase.Data;
using ExcelToDatabase.Models;
using ExcelToDatabase.Repository;
using ExcelToDatabase.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ExcelTesting
{
    public class RepositoryTesting
    {
        //variável para servir de mocking de banco de dados
        private readonly DBDataContext _dbDataContext;

        //variável para manipular as funções a ser testadas
        private readonly IBDrepository _repository;

        public RepositoryTesting()
        {
            var db = new DbContextOptionsBuilder<DBDataContext>()
                .UseInMemoryDatabase(databaseName: "MockingDatabase")
                .Options;

            _dbDataContext = new DBDataContext(db);
            _repository = new BDrepository(_dbDataContext);

            var products = new List<Products>
            {
                new Products { id = 1, name = "Product 1", price = 10, stock = 1 },
                new Products { id = 2, name = "Product 2", price = 20, stock = 1 },
                new Products { id = 3, name = "Product 3", price = 30, stock = 1 }
            };

            _dbDataContext.products.AddRange(products);
            _dbDataContext.SaveChanges();
        } 
        
        [Fact(DisplayName = "Deve verificar se um dado no banco de dados existe")]
        public async Task TestGetDataAsync()
        {
            var response = await _dbDataContext.products.ToListAsync();

            int id = response.Where(find => find.price == 20)
                .Select(find => find.id)
                .FirstOrDefault();

            Assert.Equal(2, id);
        }

        [Theory(DisplayName = "Deve verificar se os produtos foram adicionados corretamente no Banco")]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public async Task TestPostDataAsync(int id)
        {
            var products = new List<Products>
            {
                new Products { id = 4, name = "Product 4", price = 40, stock = 1 },
                new Products { id = 5, name = "Product 5", price = 50, stock = 1 },
                new Products { id = 6, name = "Product 6", price = 60, stock = 1 },
                new Products { id = 7, name = "Product 7", price = 70, stock = 1 },
                new Products { id = 8, name = "Product 8", price = 80, stock = 1 },
                new Products { id = 9, name = "Product 9", price = 90, stock = 1 }
            };

            _dbDataContext.products.AddRange(products);
            await _dbDataContext.SaveChangesAsync();

            var result = await _dbDataContext.products.ToListAsync();

            int testingResultId = result
                 .Where(find => find.id == id)
                 .Select(result => result.id)
                 .FirstOrDefault();

            Assert.Equal(id, testingResultId);
        }
    }
}