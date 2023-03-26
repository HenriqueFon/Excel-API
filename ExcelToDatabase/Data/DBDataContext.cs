using ExcelToDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelToDatabase.Data
{
    public class DBDataContext : DbContext
    {
        public DBDataContext(DbContextOptions<DBDataContext> options) : base(options) { }

        public DbSet<Products> products { get; set; }
    }
}
