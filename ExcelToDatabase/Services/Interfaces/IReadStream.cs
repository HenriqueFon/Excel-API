namespace ExcelToDatabase.Services.Interfaces
{
    public interface IReadStream
    {
        public MemoryStream CreateStream(IFormFile file);
    }
}
