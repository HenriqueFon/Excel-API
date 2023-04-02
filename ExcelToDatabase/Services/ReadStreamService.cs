using ExcelToDatabase.Services.Interfaces;

namespace ExcelToDatabase.Services
{
    public class ReadStreamService : IReadStream
    {
        public MemoryStream CreateStream(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                file?.CopyTo(stream);
                var byteArray = stream.ToArray();

                return new MemoryStream(byteArray);
            }
        }
    }
}
